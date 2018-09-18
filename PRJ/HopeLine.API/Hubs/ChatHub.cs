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

        public override Task OnConnectedAsync()
        {
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
    }
}
