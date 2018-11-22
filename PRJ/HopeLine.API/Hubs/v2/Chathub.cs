using System;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.SignalR;

namespace HopeLine.API.Hubs.v2
{
    /// <summary>
    /// This class implements signalr hub that allows user to connect
    /// </summary>
    public enum MentorStatus
    {
        Finding,
        Connected,
        Error = -1
    }

    public class ChatHub : Hub
    {
        private bool isConnected = false;
        private readonly IMessage _messageService;
        private readonly ICommunication _communicationService;
        private string CurrentRoom { get; set; }

        private static string Mentor = "MentorRoomOnly";
        public ChatHub(IMessage messageService, ICommunication communicationService)
        {
            _messageService = messageService;
            _communicationService = communicationService;
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("User has been disconnected...");
            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User has connected...");
            return base.OnConnectedAsync();
        }
        public async Task Remove(string connection, string room)
        {
            // _logger.Log(LogLevel.Information, "Removing room");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("Notify", "The other User just left.");
        }
        public async Task Connect(string connection, string room)
        {
            if (!isConnected)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                isConnected = true;
            }
            // _logger.LogInformation("Attempting to connect...");
            await Clients.Group(room).SendAsync("Connecting", connection);
        }

        public async Task Add(string room)
        {
            await Clients.Caller.SendAsync("Notify", "Adding!");
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
            }
            catch (System.Exception)
            {

                throw new Exception("Unable to add to room");
            }
            //_logger.LogInformation("Adding new Client...");
        }

        public async Task RemoveMessages(string roomId)
        {
            await _messageService.DeleteAllMessages(roomId);
        }

        public async Task LoadMessage(string room)
        {
            try
            {
                System.Console.WriteLine(room);
                var allMessages = _messageService.GetAllMessages(room);
                allMessages.Reverse();
                System.Console.WriteLine(" Count: " + allMessages.Count());
                if (allMessages != null)
                {
                    foreach (var m in allMessages)
                    {
                        await Clients.Caller.SendAsync("Load", m.UserName, m.Text);
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to Load Data: ", ex);
            }
        }

        public async Task SendMessage(string user, string message, string room)
        {
            var newmsg = new MessageModel
            {
                ConnectionId = room,
                UserName = user,
                Text = message
            };

            Console.WriteLine("Adding Message");
            _messageService.NewMessage(newmsg);
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddMentor(string mentorId)
        {
            var room = _messageService.GetRoomForUser(mentorId, false);
            System.Console.WriteLine("Room: " + room);
            if (room != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                await Clients.Caller.SendAsync("Room", room);
            }
            try
            {
                System.Console.WriteLine("Mentor available:" + mentorId);
                await Groups.AddToGroupAsync(Context.ConnectionId, Mentor);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Unable to Add Mentor to Group", ex);
            }
        }

        public async Task RemoveUser(string userId, string roomId, bool isUser)
        {
            try
            {
                System.Console.WriteLine("Removing User" + userId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

                if (isUser)
                {
                    await Clients.Group(roomId).SendAsync("NotifyMentor", userId, null, MentorStatus.Error);
                }
                else
                {
                    await Clients.Group(roomId).SendAsync("NotifyUser", MentorStatus.Error);
                }
                await _messageService.DeleteAllMessages(roomId);
            }
            catch (System.Exception ex)
            {

                throw new Exception("Delete Process did not go through :", ex);
            }
        }

        public async Task AcceptUserRequest(string mentorId, string userId, string userConnectionId)
        {
            try
            {
                var room = _communicationService.GenerateConnectionId();
                CurrentRoom = room;
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, mentorId);
                await Groups.AddToGroupAsync(userConnectionId, room);
                await Clients.Client(userConnectionId).SendAsync("Room", room);
                await _messageService.AndUsersToRoom(mentorId, userId, room);
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                await Clients.Caller.SendAsync("Room", room);
                await Clients.Group(room).SendAsync("ReceiveMessage", "system", "Hello, a mentor is here is now for you.");
            }
            catch (System.Exception ex)
            {
                throw new Exception("Unable to Accept Request: ", ex);
            }
        }

        public async Task RequestToTalk(string userId)
        {
            try
            {
                var room = _messageService.GetRoomForUser(userId, true);
                if (room != null)
                {
                    await Clients.Caller.SendAsync("NotifyUser", MentorStatus.Connected);
                    await Clients.Caller.SendAsync("Room", room);
                }
                else
                {
                    await Clients.Caller.SendAsync("NotifyUser", MentorStatus.Finding);
                    await Clients.Group(Mentor).SendAsync("NotifyMentor", userId, Context.ConnectionId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to completed request: " + ex);
                await Clients.Caller.SendAsync("NotifyUser", MentorStatus.Error);

            }

        }

        // TODO: move to own HUB
        public async Task CallDisconnected(string roomId)
        {
            await Clients.Group(roomId).SendAsync("Disconnected");
        }

        // TODO: move to own HUB
        public async Task ConnectCall(string roomId)
        {
            await Clients.Group(roomId).SendAsync("CallConnected");
        }


        // TODO: move to own HUB
        //TODO: Refactor
        public async Task RequestToVideoCall(string roomId)
        {
            Console.WriteLine("Room when requested: " + roomId);
            try
            {
                if (roomId != null)
                {

                    await Clients.Group(roomId).SendAsync("CallMentor");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to process request: ", ex);
            }
        }

    }
}
