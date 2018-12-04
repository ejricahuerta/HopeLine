using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopeLine.API.Hubs.v2 {

    public interface IChat {
        Task AddMentor (string mentorId);
        Task RequestToTalk (string userId);
        Task RemoveUser (string userId, string roomId, bool isUser);
        Task LoadMessage (string room);
        Task SendMessage (string user, string message, string room);
        Task AddTopics (string roomId, List<int> ids);
        Task Rate (string roomId, int rating);
    }
}