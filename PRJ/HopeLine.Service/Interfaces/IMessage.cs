using System.Collections.Generic;
using HopeLine.Service.Models;

namespace HopeLine.Service.Interfaces
{
    public interface IMessage
    {
        void NewMessage(MessageModel model);
        void DeleteAllMessages(string connectionId);
        IEnumerable<MessageModel> GetAllMessages(string connectionId);

    }
}