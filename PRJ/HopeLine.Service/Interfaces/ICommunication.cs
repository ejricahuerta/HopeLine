using System.Collections.Generic;
using HopeLine.Service.Models;

namespace HopeLine.Service.Interfaces {
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunication {
        //For Admin Only
        IEnumerable<ConversationModel> GetConversations ();

        //For Mentor Only
        IEnumerable<ConversationModel> GetConversationsByMentorId (string mentorId);

        //For Registered User Only
        IEnumerable<ConversationModel> GetConversationsByUserId (string userId);

        //For all kind of users
        ConversationModel GetConversationById (int id);
        ConversationModel GetConversationByPIN (string pin);

        bool EditConversation (ConversationModel conversation);

        string GenerateConnectionId ();

        bool AddConversation (ConversationModel conversation);

    }
}