using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Security.Interfaces;

namespace HopeLine.Security.Services
{
    //TODO : add ref to user manager and add logic
    public class UserService : IUserService
    {
        private readonly IRepository<HopeLineUser> _repository;

        public UserService(IRepository<HopeLineUser> repository)

        {
            _repository = repository;
        }
    }
}
