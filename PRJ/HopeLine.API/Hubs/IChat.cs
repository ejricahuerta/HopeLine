using System;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.SignalR;

namespace HopeLine.API.Hubs.v2
{

    public interface IChat
    {
        Task AddMentor(string mentorId);
        Task RequestToTalk(string userId);
        Task RemoveUser(string userId, string roomId, bool isUser);
        Task LoadMessage(string room);
        Task SendMessage(string user, string message, string room);
    }
}