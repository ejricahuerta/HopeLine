using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HopeLine.API.Hubs
{
    //TODO : add implementation for signal r core messaging

    /// <summary>
    /// 
    /// </summary>

    public class ChatHub : Hub
    {
        private readonly ICommunication _communicationService;

        public ChatHub(ICommunication communicationService)
        {

            _communicationService = communicationService;
        }
    
        public async Task AddUserToRoom(string room)
        {   
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            System.Console.WriteLine("Added User");
        }

        public async Task RemoveUserFromRoom(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }


        public async Task SendMessage(string user, string message, string room)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }
    }
}
