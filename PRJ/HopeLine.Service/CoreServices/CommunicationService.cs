//using HopeLine.Service.Interfaces;

using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System;
using System.Collections.Generic;

namespace HopeLine.Service.CoreServices
{
    //TODO : change repo to unit of work.
    public class CommunicationService : ICommunication
    {
        private readonly IRepository<HopeLineUser> _userRepo;
        private readonly IRepository<Conversation> _conversationRepo;

        public CommunicationService(IRepository<HopeLineUser> userRepo,
                                    IRepository<Conversation> conversationRepo)
        {
            _userRepo = userRepo;
            _conversationRepo = conversationRepo;
        }

        public bool AddConversation(ConversationModel conversation, bool isGuestUser)
        {
            throw new System.NotImplementedException();
        }

        public bool AddConversation(ConversationModel conversation)
        {
            throw new System.NotImplementedException();
        }

        public bool EditConversation(ConversationModel conversation)
        {
            throw new System.NotImplementedException();
        }

        public string GenerateConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public ConversationModel GetConversationById(int id)
        {
            throw new System.NotImplementedException();
        }

        public ConversationModel GetConversationByPIN(string pin)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ConversationModel> GetConversations()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ConversationModel> GetConversationsByMentorId(string mentorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ConversationModel> GetConversationsByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
