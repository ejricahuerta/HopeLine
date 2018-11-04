using System;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.SignalR;

namespace HopeLine.API.Hubs {
    /// <summary>
    /// This class implements signalr hub that allows user to connect
    /// </summary>

    public class ChatHub : Hub {
        private readonly IMessage _messageService;
        private readonly ICommunication _communicationService;
        public ChatHub (IMessage messageService, ICommunication communicationService) {
            _messageService = messageService;
            _communicationService = communicationService;
        }

        public override async Task OnDisconnectedAsync (Exception exception) {
            await base.OnDisconnectedAsync (exception);
        }

        /// <summary>
        /// Invoke this method whenever the mentor accepted the convo
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task AddUserToRoom (string room) {
            await Groups.AddToGroupAsync (Context.ConnectionId, room);
            System.Console.WriteLine ("Added User: " + Context.ConnectionId);
        }

        /// <summary>
        /// Load all messages for the current users
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task LoadMessage (string room) {
            try {
                System.Console.WriteLine (room);
                var allMessages = _messageService.GetAllMessages (room);
                System.Console.WriteLine (" Count: " + allMessages.Count ());
                if (allMessages != null) {
                    foreach (var m in allMessages) {
                        await Clients.Caller.SendAsync ("Load", m.UserName, m.Text);
                    }
                }
            } catch (System.Exception ex) {

                throw new System.Exception ("Unable to Load Data: ", ex);
            }
        }

        /// <summary>
        /// Send a normal chat message
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task SendMessage (string user, string message, string room) {
            var newmsg = new MessageModel {
                ConnectionId = room,
                UserName = user,
                Text = message
            };

            Console.WriteLine("Adding Message");

            _messageService.NewMessage (newmsg);
            await Clients.Group (room).SendAsync ("ReceiveMessage", user, message);
        }

        #region USERS

        /// <summary>
        /// Invoke when user click "Talk to Mentor"
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UserRequestToTalk (string user) {
            //List all available mentors
            var availablementors = _messageService.ListAvailableMentor ()
                .Select (o => o.ConnectionId)
                .ToList ()
                .AsReadOnly ();

            if (availablementors == null) {
                //Notify User 
                await Clients.Caller
                    .SendAsync ("NotifyUser", "There are no Available Mentors at the moment. Please try again later.");
            } else {
                //Notify Mentor and User
                await Clients.Clients (availablementors)
                    .SendAsync ("NotifyMentor", string.Format ("{0} is looking for company...", Context.ConnectionId));
                await Clients.Caller.SendAsync ("NotifyUser", "Matching to Mentor...");
            }
        }

        /// <summary>
        /// invoke this class whenever the user disconnects
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task RemoveUserFromRoom (string room) {
            try {
                _messageService.DeleteAllMessages (room);
                await Groups.RemoveFromGroupAsync (Context.ConnectionId, room);
                await Clients.Caller.SendAsync ("Load", "System", "User has been disconnected.");
            } catch (System.Exception ex) {
                throw new System.Exception ("Unable to Remove User Room: ", ex);
            }
        }

        #endregion

        #region MENTORS

        /// <summary>
        /// Add Mentor whenever a mentor is online
        /// </summary>
        /// <param name="mentorId"></param>
        /// <returns></returns>
        public async Task AddMentor (string mentorId) {
            System.Console.WriteLine ("Adding Mentor to Online Mentors...");
            try {
                await _messageService.NewMentorAvailable (mentorId, Context.ConnectionId);
                System.Console.WriteLine ("finished adding mentor...");
            } catch (System.Exception ex) {
                throw new System.Exception ("Unable to Process New Mentor: ", ex);
            }
        }

        /// <summary>
        /// Invoke after mentor is notified
        /// </summary>
        /// <param name="mentorId"></param>
        /// <param name="userConnectionId"></param>
        /// <returns></returns>
        /// 
        // TODO: make sure only one mentor accepts

        public async Task MentorAcceptRequest (string mentorId, string userConnectionId) {
            try {
                //Generate room

                var room = _communicationService.GenerateConnectionId ();

                //set Mentor unavailable
                await _messageService.SetMentorOnCall (mentorId, Context.ConnectionId);

                //Add user and mentor to room
                await Groups.AddToGroupAsync (Context.ConnectionId, room);
                await Groups.AddToGroupAsync (userConnectionId, room);

                //Notify User
                await Clients.Client (userConnectionId).SendAsync ("NotifyUser", "Connected.");
                await Clients.Caller.SendAsync ("NotifyMentor", "Connected");
            } catch (System.Exception ex) {
                throw new Exception ("Mentor Unable to Accept: ", ex);
            }
        }
        #endregion MENTORS
    }
}