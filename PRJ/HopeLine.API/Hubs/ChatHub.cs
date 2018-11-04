using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.API.Hubs
{
    /// <summary>
    /// This class implements signalr hub that allows user to connect
    /// </summary>

    public class ChatHub : Hub
    {
        private readonly IMessage _messageService;
        private readonly ICommunication _communicationService;

        public ChatHub(IMessage messageService, ICommunication communicationService)
        {
            _messageService = messageService;
            _communicationService = communicationService;

        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

        }


        //Invoke this method wherever the mentor accepted
        public async Task AddUserToRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            System.Console.WriteLine("Added User: " + Context.ConnectionId);
        }

        public async Task RemoveUserFromRoom(string room)
        {
            try
            {
                _messageService.DeleteAllMessages(room);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
                await Clients.Caller.SendAsync("Load", "System", "User has been disconnected.");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Remove User Room: ", ex);
            }
        }

        public async Task LoadMessage(string room)
        {
            try
            {
                System.Console.WriteLine(room);
                var allMessages = _messageService.GetAllMessages(room);
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
            _messageService.NewMessage(newmsg);
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddMentor(string mentorId)
        {
            System.Console.WriteLine("Adding Mentor to Online Mentors...");
            try
            {
                await _messageService.NewMentorAvailable(mentorId);
                System.Console.WriteLine("finished adding mentor...");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Process New Mentor: ", ex);
            }
        }

        public async Task UserRequestToTalk(string user)
        {

            //List all available mentors
            var availablementors = _messageService.ListAvailableMentor()
                                    .Select(o => o.ConnectionId)
                                    .ToList()
                                    .AsReadOnly();

            if (availablementors == null)
            {

                //Notify User 
                await Clients.Caller
                .SendAsync("NotifyUser", "There are no Available Mentors at the moment. Please try again later.");
            }
            else
            {

                //Notify Mentor and User
                await Clients.Clients(availablementors)
                            .SendAsync("NotifyMentor", string.Format("{0} is looking for company...", Context.ConnectionId));
                await Clients.Caller.SendAsync("NotifyUser", "Matching to Mentor...");
            }
        }

        public async Task MentorAcceptRequest(string mentorId, string userConnectionId)
        {
            try
            {
                //Generate room
                var room = _communicationService.GenerateConnectionId();

                //set Mentor unavailable
                await _messageService.SetMentorOnCall(mentorId, Context.ConnectionId);

                //Add user and mentor to room
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                await Groups.AddToGroupAsync(userConnectionId, room);

                //Notify User
                await Clients.Client(userConnectionId).SendAsync("NotifyUser", "Connected.");
            }
            catch (System.Exception ex)
            {
                throw new Exception("Mentor Unable to Accept: ", ex);
            }
        }

        
    }
}
