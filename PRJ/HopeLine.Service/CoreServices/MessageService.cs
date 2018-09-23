using System.Collections.Generic;
using System.Linq;
using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;

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
                        .Where(m=>m.ConnectionId == connectionId)
                        .Select(mm=> new MessageModel{
                            ConnectionId = mm.ConnectionId,
                            UserName = mm.UserName,
                            Text = mm.Text
                        });
            }
            catch (System.Exception ex)
            {
                
                throw new System.Exception("Unable to Get Messages: ",ex);
            }
        }
        
        public void NewMessage(MessageModel model)
        {
            try
            {
                _chatDb.Messages.Add(new Message{
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
    }
}