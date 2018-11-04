using System.Collections.Generic;
using System.Threading.Tasks;
using HopeLine.Service.Models;

namespace HopeLine.Service.Interfaces {
    public interface IMessage {
        void NewMessage (MessageModel model);

        Task NewMentorAvailable (string mentorId, string connectionId);
        Task RemoveMentor (string connectionId);
        void DeleteAllMessages (string connectionId);
        IEnumerable<MessageModel> GetAllMessages (string connectionId);
        IEnumerable<OnlineMentorModel> ListAvailableMentor ();
        Task SetMentorOnCall (string mentorId, string connectionId);

    }
}