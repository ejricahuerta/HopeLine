using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HopeLine.API.Hubs
{
    //TODO : add implementation for signal r core messaging

    /// <summary>
    /// 
    /// </summary>
    /// 
    [Authorize]

    public class ChatHub : Hub
    {
        private readonly ICommunication _communicationService;

        public ChatHub(ICommunication communicationService)
        {
            _communicationService = communicationService;
        }
        public override Task OnConnectedAsync()
        {

            Console.WriteLine("Mentor Connected");
            _communicationService.AddConversation(new ConversationModel
            {
                DateOfConversation = DateTime.UtcNow,
                // connection ID
                //
            });

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public Task SendToMentor(string mentor, string message)
        {
            return Clients.User(mentor).SendAsync(message);
        }

        public async Task AddUserToRoom(string groupName)
        {
            Console.WriteLine("Created new group: Room ID");
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", "User Joined");

        }

        public async Task RemoveUserFromRoom(string groupName) {

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", "User Left.");
        }

    }
}
