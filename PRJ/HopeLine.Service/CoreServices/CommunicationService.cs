//using HopeLine.Service.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;

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
        public bool AddConversation(ConversationModel conversation)
        {
            try
            {
                var newConversation = new Conversation
                {
                    PIN = conversation.PIN,
                    UserId = conversation.UserId,
                    MentorId = conversation.MentorId,
                    DateOfConversation = conversation.DateOfConversation.ToString(),
                    Rating = (int)conversation.Rating
                };
                _conversationRepo.Insert(newConversation);
                _conversationRepo.Save();
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
                var oldConversation = _conversationRepo.Get(conversation.Id);
                oldConversation.PIN = conversation.PIN;
                oldConversation.UserId = conversation.UserId;
                oldConversation.MentorId = conversation.MentorId;
                oldConversation.DateOfConversation = conversation.DateOfConversation.ToString();
                oldConversation.Rating = (int)conversation.Rating;

                _conversationRepo.Update(oldConversation);
                _conversationRepo.Save();
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
                var obj = _conversationRepo.Get(id);
                return new ConversationModel
                {
                    Id = obj.Id,
                    PIN = obj.PIN,
                    MentorId = obj.MentorId,
                    UserId = obj.UserId,
                    DateOfConversation = DateTime.Parse(obj.DateOfConversation),
                };

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
                var obj = _conversationRepo.GetAll(null)
                    .SingleOrDefault(c => c.PIN == pin);
                return (obj != null) ? new ConversationModel
                {
                    Id = obj.Id,
                    PIN = obj.PIN,
                    UserId = obj.UserId,
                    MentorId = obj.MentorId,
                    DateOfConversation = DateTime.Parse(obj.DateOfConversation),
                } : null;

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
                var conversations = _conversationRepo.GetAll(null)
                    .Select(c => new ConversationModel
                    {
                        Id = c.Id,
                        PIN = c.PIN,
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
                return _conversationRepo.GetAll(null)
                    .Where(c => c.MentorId == mentorId)
                    .Select(c =>
                       new ConversationModel
                       {
                           Id = c.Id,
                           PIN = c.PIN,
                           MentorId = c.MentorId,
                           UserId = c.UserId,
                           DateOfConversation = DateTime.Parse(c.DateOfConversation),
                       }
                    );

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
                return _conversationRepo.GetAll(null)
                    .Where(c => c.UserId.ToString().Contains(userId))
                    .Select(c =>
                       new ConversationModel
                       {
                           Id = c.Id,
                           PIN = c.PIN,
                           MentorId = c.MentorId,
                           UserId = c.UserId,
                           DateOfConversation = DateTime.Parse(c.DateOfConversation),
                       }
                    );
            }
            catch (SystemException e)
            {
                Console.WriteLine("SERVICE ERROR:" + e);
            }
            return null;
        }
    }
}