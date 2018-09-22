using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HopeLine.API.Hubs
{

    /// <summary>
    /// This class implements signalr hub that allows user to connect
    /// </summary>

    public class ChatHub : Hub
    {
        private readonly IMessage _messageService;

        public ChatHub(IMessage messageService)
        {
            _messageService = messageService;
        }

        public async Task AddUserToRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            System.Console.WriteLine("Added User");
        }

        public async Task RemoveUserFromRoom(string room)
        {
            try
            {
                _messageService.DeleteAllMessages(room);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Remove User Room: ", ex);
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
            _messageService.NewMessage(newmsg);
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }
    }
}
