//using HopeLine.Service.Interfaces;

using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
            }
            return false;
        }

        public bool AddConversation(ConversationModel conversation)
        {
            try
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
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }

        }

        public bool EditConversation(ConversationModel conversation)
        {
            try
            {
                var _conversation = new Conversation
                {
                    PIN = conversation.PIN,
                    Minutes = conversation.Minutes,
                    Mentor = conversation.Mentor,
                    DateOfConversation = conversation.DateOfConversation,
                    LanguageUsed = conversation.LanguageUsed
                };
                _conversationRepo.Update(_conversation);
                return true;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
        }

        public string GenerateConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public ConversationModel GetConversationById(int id)
        {
            try
            {
                var obj = _conversationRepo.GetAll()
                     .Select(c =>

                         new ConversationModel
                         {
                             PIN = c.PIN,
                             Minutes = c.Minutes,
                             Mentor = c.Mentor,
                             DateOfConversation = c.DateOfConversation,
                             LanguageUsed = c.LanguageUsed

                         }
                     )
                     .SingleOrDefault(c => c.Id == id);

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }

        public ConversationModel GetConversationByPIN(string pin)
        {
            try
            {
                var obj = _conversationRepo.GetAll()
                    .Select(c =>

                        new ConversationModel
                        {
                            Minutes = c.Minutes,
                            Mentor = c.Mentor,
                            DateOfConversation = c.DateOfConversation,
                            LanguageUsed = c.LanguageUsed

                        }
                    )
                    .SingleOrDefault(c => c.PIN == pin);

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }

        public IEnumerable<ConversationModel> GetConversations()
        {
            try
            {
                var conversations = (_conversationRepo as IEnumerable<Conversation>)
                    .Select(c => new ConversationModel
                    {
                        PIN = c.PIN,
                        Minutes = c.Minutes,
                        Mentor = c.Mentor,
                        DateOfConversation = c.DateOfConversation,
                        LanguageUsed = c.LanguageUsed
                    });

                return conversations;

            }
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }

        public IEnumerable<ConversationModel> GetConversationsByMentorId(string mentorId)
        {
            try
            {
                var obj = _conversationRepo.GetAll()
                    .Where(c => c.Mentor.Id.ToString().Contains(mentorId))
                    .Select(c =>

                        new ConversationModel
                        {
                            PIN = c.PIN,
                            Minutes = c.Minutes,
                            Mentor = c.Mentor,
                            DateOfConversation = c.DateOfConversation,
                            LanguageUsed = c.LanguageUsed

                        }
                    );

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }

        public IEnumerable<ConversationModel> GetConversationsByUserId(string userId)
        {
            try
            {
                var obj = _conversationRepo.GetAll()
                    .Where(c => c.UserId.ToString().Contains(userId))
                    .Select(c =>

                        new ConversationModel
                        {
                            PIN = c.PIN,
                            Minutes = c.Minutes,
                            Mentor = c.Mentor,
                            DateOfConversation = c.DateOfConversation,
                            LanguageUsed = c.LanguageUsed

                        }
                    );

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }
    }
}
