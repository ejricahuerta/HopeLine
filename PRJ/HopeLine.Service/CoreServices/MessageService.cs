using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Service.CoreServices
{
    public class MessageService : IMessage
    {
        private readonly ChatDbContext _chatDb;

        public MessageService(ChatDbContext chatDb)
        {
            _chatDb = chatDb;
        }
        public void DeleteAllMessages(string connectionId)
        {
            try
            {
                var connectionMessages = _chatDb.Messages
                    .Where(m => m.ConnectionId == connectionId);
                foreach (var m in connectionMessages)
                {
                    _chatDb.Remove(m);
                }
                _chatDb.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to Remove Messages: ", ex);
            }
        }

        public IEnumerable<MessageModel> GetAllMessages(string connectionId)
        {
            try
            {
                //TODO  : change to logger
                System.Console.WriteLine("Returning All Messages for " + connectionId);
                return _chatDb.Messages
                    .Where(m => m.ConnectionId == connectionId)
                    .Select(mm => new MessageModel
                    {
                        ConnectionId = mm.ConnectionId,
                        UserName = mm.UserName,
                        Text = mm.Text
                    });
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to Get Messages: ", ex);
            }
        }

        public IEnumerable<OnlineMentorModel> ListAvailableMentor()
        {
            try
            {
                return _chatDb.OnlineMentors.Where(m => m.Available == true).Select(n => new OnlineMentorModel
                {
                    Available = n.Available,
                    ConnectionId = n.ConnectionId,
                    Id = n.Id,
                    MentorId = n.MentorId
                });
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to Process Finding Mentors : ", ex);
            }
        }

        public async Task NewMentorAvailable(string mentorId, string connectionId)
        {
            try
            {
                var newOnline = new OnlineMentor
                {
                    MentorId = mentorId,
                    ConnectionId = connectionId,
                    Available = true
                };
                var mentor = _chatDb.OnlineMentors.SingleOrDefault(i => i.MentorId == mentorId);
                if (mentor == null)
                {
                    await _chatDb.OnlineMentors.AddAsync(newOnline);
                    await _chatDb.SaveChangesAsync();
                }
                else
                {
                    mentor.ConnectionId = connectionId;
                    _chatDb.Update(mentor);
                    _chatDb.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Add Mentor to Online Mentors: ", ex);
            }
        }

        public void NewMessage(MessageModel model)
        {
            try
            {
                _chatDb.Messages.Add(new Message
                {
                    ConnectionId = model.ConnectionId,
                    UserName = model.UserName,
                    Text = model.Text
                });

                _chatDb.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to save new Message: ", ex);
            }
        }

        public async Task RemoveMentor(string mentorId)
        {
            try
            {
                var mentor = _chatDb.OnlineMentors.SingleOrDefault(m => m.MentorId == mentorId);
                _chatDb.Remove(mentor);
                await _chatDb.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Remove Mentor From Pool: ", ex);
            }
        }

        public async Task SetMentorOnCall(string mentorId, string connectionId)
        {
            try
            {
                var mentor = _chatDb.OnlineMentors.SingleOrDefault(i => i.MentorId == mentorId);
                mentor.Available = false;
                mentor.ConnectionId = connectionId;
                _chatDb.Update(mentor);
                await _chatDb.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Set Mentor Offline: ", ex);
            }
        }
    }
}