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
        //public override Task OnConnectedAsync()
        //{

        //    Console.WriteLine("Mentor Connected");
        //    _communicationService.AddConversation(new ConversationModel
        //    {
        //        DateOfConversation = DateTime.UtcNow,
        //        // connection ID
        //        //
        //    });

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //}

        //public Task SendToMentor(string mentor, string message)
        //{
        //    return Clients.User(mentor).SendAsync(message);
        //}

        //public async Task AddUserToRoom(string groupName)
        //{
        //    Console.WriteLine("Created new group: Room ID");
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).SendAsync("Send", "User Joined");

        //}

        //public async Task RemoveUserFromRoom(string groupName)
        //{

        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).SendAsync("Send", "User Left.");
        //}
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
