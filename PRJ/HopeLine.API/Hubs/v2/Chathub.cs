using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HopeLine.API.Hubs.v2 {
    /// <summary>
    /// This class implements signalr hub that allows user to connect
    /// </summary>
    public enum MentorStatus {
        Finding = 0,
        Connected = 1,
        Error = -1
    }

    public class ChatHub : Hub, IChat, ICall {
        private bool isConnected = false;

        public readonly ILogger<ChatHub> _logger;
        private readonly IMessage _messageService;
        private readonly ICommunication _communicationService;
        private readonly ICommonResource _commonResource;

        private string CurrentRoom { get; set; }

        private DateTime DateConversationStarted { get; set; }
        private static string Mentor = "MentorRoomOnly";
        public ChatHub (ILogger<ChatHub> logger, IMessage messageService, ICommunication communicationService, ICommonResource commonResource) {
            _logger = logger;
            _messageService = messageService;
            _communicationService = communicationService;
            _commonResource = commonResource;
        }

        public override Task OnDisconnectedAsync (Exception exception) {
            _logger.LogInformation ("User has been disconnected...");
            return base.OnDisconnectedAsync (exception);
        }

        public override Task OnConnectedAsync () {
            _logger.LogInformation ("User has connected...");
            return base.OnConnectedAsync ();
        }

        public async Task LoadMessage (string room) {
            try {
                _logger.LogInformation ("Loading messages for {}", room);
                var allMessages = _messageService.GetAllMessages (room);
                //allMessages.Reverse();
                System.Console.WriteLine (" Count: " + allMessages.Count ());
                if (allMessages != null) {
                    foreach (var m in allMessages) {
                        await Clients.Caller.SendAsync ("ReceiveMessage", m.UserName, m.Text);
                    }
                }
            } catch (System.Exception ex) {
                _logger.LogCritical ("Unable to Load Data: ", ex);
            }
        }

        public async Task SendMessage (string user, string message, string room) {
            var newmsg = new MessageModel {
                ConnectionId = room,
                UserName = user,
                Text = message
            };
            _logger.LogInformation ("Adding message for " + user);
            _messageService.NewMessage (newmsg);
            await Clients.Group (room).SendAsync ("ReceiveMessage", user, message);
        }

        public async Task AddMentor (string mentorId) {
            var room = _messageService.GetRoomForUser (mentorId, false);
            if (room != null) {
                await Groups.AddToGroupAsync (Context.ConnectionId, room);
                await Clients.Caller.SendAsync ("Room", room);
            }

            try {
                await Groups.AddToGroupAsync (Context.ConnectionId, Mentor);
                _logger.LogInformation ("Added mentor to room: " + room);
            } catch (System.Exception ex) {
                throw new Exception ("Unable to Add Mentor to Group", ex);
            }
        }

        public async Task RemoveUser (string userId, string roomId, bool isUser) {
            try {
                _logger.LogInformation ("Removing User from room", roomId);
                await Groups.RemoveFromGroupAsync (Context.ConnectionId, roomId);

                if (isUser) {
                    await Clients.Group (roomId).SendAsync ("NotifyMentor", userId, null, MentorStatus.Error);
                } else {
                    await Clients.Group (roomId).SendAsync ("NotifyUser", MentorStatus.Error);
                }
                await _messageService.DeleteAllMessages (roomId);
            } catch (System.Exception ex) {
                _logger.LogCritical ("Unable to Proceed", ex);
            }

            //Updating Conversation
            if (CurrentRoom != null) {
                var conversation = _communicationService.GetConversationByPIN (CurrentRoom);
                TimeSpan span = (DateTime.Now - conversation.DateOfConversation);

                conversation.Minutes = (float) span.TotalSeconds;
                var result = _communicationService.EditConversation (conversation);
                if (!result) {
                    _logger.LogWarning ("Unable to Proceed", CurrentRoom);
                }
            }
        }

        public async Task AcceptUserRequest (string mentorId, string userId, string userConnectionId) {

            var room = _communicationService.GenerateConnectionId ();
            try {
                CurrentRoom = room;
                await Groups.RemoveFromGroupAsync (Context.ConnectionId, Mentor);
                await Groups.AddToGroupAsync (userConnectionId, room);
                await Clients.Client (userConnectionId).SendAsync ("Room", room);
                await _messageService.AndUsersToRoom (mentorId, userId, room);
                await Groups.AddToGroupAsync (Context.ConnectionId, room);
                await Clients.Caller.SendAsync ("Room", room);
                await Clients.Group (room).SendAsync ("ReceiveMessage", "HopeLine", "Welcome to HopeLine Chatroom");
            } catch (System.Exception ex) {
                _logger.LogCritical ("Unable to Accept Request.", room, userId, ex);
            }

            //Creating new Conversation
            var result = _communicationService.AddConversation (new ConversationModel {
                PIN = room,
                    UserId = userId,
                    MentorId = mentorId,
                    DateOfConversation = DateTime.Now
            });

            if (!result) {
                _logger.LogWarning ("Adding Conversation Failed", room, userId);
            } else {
                _logger.LogInformation ("Adding Conversation", room, userId);
            }
        }

        public async Task RequestToTalk (string userId) {
            try {
                var room = _messageService.GetRoomForUser (userId, true);
                if (room != null) {
                    await Groups.AddToGroupAsync (Context.ConnectionId, room);
                    await Clients.Caller.SendAsync ("Room", room);
                    await Clients.Caller.SendAsync ("NotifyUser", MentorStatus.Connected);
                } else {
                    await Clients.Caller.SendAsync ("NotifyUser", MentorStatus.Finding);
                    await Clients.Group (Mentor).SendAsync ("NotifyMentor", userId, Context.ConnectionId);
                }
            } catch (Exception ex) {
                _logger.LogInformation ("Unable to Process Request for User: {} with Error: {}", userId, ex);
                await Clients.Caller.SendAsync ("NotifyUser", MentorStatus.Error);
            }
        }

        public async Task CallDisconnected (string roomId) {
            _logger.LogInformation ("Call has been disconnected");
            await Clients.Group (roomId).SendAsync ("Disconnected");
        }

        public async Task ConnectCall (string roomId) {
            _logger.LogInformation ("Call Connected.");
            await Clients.Group (roomId).SendAsync ("CallConnected");

        }

        public async Task RequestToVideoCall (string roomId) {
            _logger.LogInformation ("A request from room occured.", roomId);
            try {
                if (roomId != null) {
                    await Clients.Group (roomId).SendAsync ("CallMentor");
                }

            } catch (Exception ex) {
                _logger.LogWarning ("Unable to proceed request.", roomId, ex);
            }
        }

        public async Task AddTopics (string roomId, IList<int> ids) {
            if (ids.Count () != 0) {
                try {
                    //get topics from service
                    _logger.LogInformation ("Fetching all Topics by ids: {}", ids);
                    var allTopics = _commonResource.GetTopics ();
                    var topics = new List<TopicModel> ();
                    foreach (var id in ids) {
                        var topic = allTopics.FirstOrDefault (t => t.Id == id);
                        if (topic != null) {
                            topics.Add (topic);
                        }
                    }
                    await Clients.Group (roomId).SendAsync ("Topics", topics.Select (t => t.Name));
                }
            } catch (Exception) {
                _logger.LogWarning ("Unable to Fetch Topics");

            }
        } else {
            _logger.LogInformation ("no Topics Selected");
        }
    }

    public async Task Rate (string roomId, int rating) {
        var conversation = _communicationService.GetConversationByPIN (roomId);
        if (conversation == null) {
            Rating rate = (Rating) rating;
            conversation.Rating = rate;
            _communicationService.EditConversation (conversation);
        }

    }
}
}