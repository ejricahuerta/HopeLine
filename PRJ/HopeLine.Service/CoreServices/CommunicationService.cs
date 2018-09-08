//using HopeLine.Service.Interfaces;

using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;

namespace HopeLine.Service.CoreServices
{
    //TODO : change repo to unit of work.
    public class CommunicationService //: ICommunication
    {
        private readonly IRepository<HopeLineUser> _userRepo;
        private readonly IRepository<Conversation> _conversationRepo;

        public CommunicationService(IRepository<HopeLineUser> userRepo,
                                    IRepository<Conversation> conversationRepo)
        {
            _userRepo = userRepo;
            _conversationRepo = conversationRepo;
        }
    }
}
