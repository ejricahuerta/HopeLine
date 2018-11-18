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

    public class ChatHub : Hub
    {
        private readonly IMessage _messageService;
        private readonly ICommunication _communicationService;

        private string Mentor = "MentorRoomOnly";
        public ChatHub(IMessage messageService, ICommunication communicationService)
        {
            _messageService = messageService;
            _communicationService = communicationService;
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

        }
        public async Task Remove(string connection, string room)
        {
            // _logger.Log(LogLevel.Information, "Removing room");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("Notify", "The other User just left.");
        }
        public async Task Connect(string connection, string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
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
                    /*
                    foreach (var m in allMessages.ToList().Reverse())
                    {
                        await Clients.Caller.SendAsync("Load", m.UserName, m.Text);
                    }*/
                    foreach (var m in allMessages.Reverse())
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
                var method = (isUser) ? "NotifyUser" : "NotifyMentor";
                await Clients.Group(roomId).SendAsync(method, -1);
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
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, mentorId);
                await Groups.AddToGroupAsync(userConnectionId, room);
                await Clients.Client(userConnectionId).SendAsync("Room", room);
                await _messageService.AndUsersToRoom(mentorId, userId, room);
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                await Clients.Caller.SendAsync("Room", room);
                await Clients.Group(room).SendAsync("ReceiveMessage", "system", "Welcome " + userId);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Unable to Accept Request: ", ex);
            }
        }

        public async Task RequestToTalk(string userId)
        {
            var room = _messageService.GetRoomForUser(userId, true);
            if (room != null)
            {
                await Clients.Caller.SendAsync("NotifyUser", 1);
                await Clients.Caller.SendAsync("Room", room);
            }
            else
            {
                await Clients.Caller.SendAsync("NotifyUser", 0);
                await Clients.Group(Mentor).SendAsync("NotifyMentor", userId, Context.ConnectionId);
            }
        }
    }
}
