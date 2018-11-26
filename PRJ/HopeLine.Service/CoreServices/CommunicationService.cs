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
                    var newConversation = new Conversation
                    {
                        PIN = conversation.PIN,
                        MentorId = conversation.MentorId,
                        UserId = conversation.UserId,
                        Minutes = conversation.Minutes,
                        DateOfConversation = conversation.DateOfConversation.ToString(),
                    };
                    _conversationRepo.Insert(newConversation);
                    return true;
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR: " + e);
            }
            return false;
        }

        public bool AddConversation(ConversationModel conversation)
        {
            try
            {
                var newConversation = new Conversation
                {
                    PIN = conversation.PIN,
                    UserId = conversation.UserId,
                    Minutes = conversation.Minutes,
                    MentorId = conversation.MentorId,
                    DateOfConversation = conversation.DateOfConversation.ToString(),
                };
                _conversationRepo.Insert(newConversation);
                return true;
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR: " + e);
            }
            return false;

        }

        public bool EditConversation(ConversationModel conversation)
        {
            try
            {
                var newConversation = new Conversation
                {
                    PIN = conversation.PIN,
                    UserId = conversation.UserId,
                    Minutes = conversation.Minutes,
                    MentorId = conversation.MentorId,
                    DateOfConversation = conversation.DateOfConversation.ToString()
                };
                _conversationRepo.Update(newConversation);
                return true;
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR: " + e);
            }
            return false;
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
                             MentorId = c.MentorId,
                             UserId = c.UserId,
                             DateOfConversation = DateTime.Parse(c.DateOfConversation),
                         }
                     )
                     .SingleOrDefault(c => c.Id == id);

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR: " + e);
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
                            PIN = c.PIN,
                            UserId = c.UserId,
                            Minutes = c.Minutes,
                            MentorId = c.MentorId,
                            DateOfConversation = DateTime.Parse(c.DateOfConversation),


                        }
                    )
                    .SingleOrDefault(c => c.PIN == pin);

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR:" + e);
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
                        MentorId = c.MentorId,
                        UserId = c.UserId,
                        DateOfConversation = DateTime.Parse(c.DateOfConversation),

                    });

                return conversations;

            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR: " + e);
            }
            return null;
        }

        public IEnumerable<ConversationModel> GetConversationsByMentorId(string mentorId)
        {
            try
            {
                var obj = _conversationRepo.GetAll()
                    .Where(c => c.MentorId == mentorId)
                    .Select(c =>
                        new ConversationModel
                        {
                            PIN = c.PIN,
                            Minutes = c.Minutes,
                            MentorId = c.MentorId,
                            UserId = c.UserId,
                            DateOfConversation = DateTime.Parse(c.DateOfConversation),
                        }
                    );

                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR:" + e);
            }
            return null;
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
                            MentorId = c.MentorId,
                            UserId = c.UserId,
                            DateOfConversation = DateTime.Parse(c.DateOfConversation),
                        }
                    );
                return obj;
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR:" + e);
            }
            return null;
        }
    }
}
