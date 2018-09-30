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
            try
            {
                if (isGuestUser)
                {
                    var _conversation = new Conversation
                    {
                        PIN = conversation.PIN,
                        Minutes = conversation.Minutes,
                        Mentor = conversation.Mentor,
                        DateOfConversation = conversation.DateOfConversation,
                        LanguageUsed = conversation.LanguageUsed
                    };
                    _conversationRepo.Insert(_conversation);
                    return true;
                }
            }
            catch(SystemException e)
            {
                Console.WriteLine("Error: " + e);
            }
            return false;
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
