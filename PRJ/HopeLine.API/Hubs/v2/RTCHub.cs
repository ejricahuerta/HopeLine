using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.API.Hubs.v2
{
    public class RTCHub : Hub
    {
        private readonly ICommunication _communicationService;
        public string PeerId { get; set; }

        public string RoomId  { get; set; }
        public RTCHub(ICommunication communicationService)
        {
            _communicationService = communicationService;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User has connected..");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("User has disconnected...");
            return base.OnDisconnectedAsync(exception);
        }

        ///AddUsers PeerId 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task AddPeer(string id, string room)
        {
            PeerId = (id == null) ? _communicationService.GenerateConnectionId() : id;
            RoomId = room;
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("Peer", PeerId);
        }
    }
}
