using System.Collections.Generic;
using System.Threading.Tasks;
using HopeLine.Service.Models;

namespace HopeLine.Service.Interfaces {
    public interface IMessage {
        void NewMessage (MessageModel model);

        Task NewMentorAvailable (string mentorId, string connectionId);
        Task RemoveMentor (string connectionId);
        Task DeleteAllMessages (string roomId);
        IEnumerable<MessageModel> GetAllMessages (string roomId);
        IEnumerable<OnlineMentorModel> ListAvailableMentor ();
        Task SetMentorOnCall (string mentorId, string connectionId);

        string GetRoomForUser (string userId, bool isGuest);
        Task AndUsersToRoom (string mentorId, string guestId, string roomId);

        void AddTopicsToRoom (string roomId, IList<int> topics);
        IList<int> GetTopics (string roomId);
    }
}