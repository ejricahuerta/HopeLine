using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System.Collections.Generic;

namespace HopeLine.Service.CoreServices
{
    public class UserService : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserModel> GetAllUsersByAccountType(HopeLineUser.Account accountType)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ActivityModel> GetMentorActivities(string mentorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ConversationModel> GetMentorConversations(string mentorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ScheduleModel> GetMentorSchedules(string mentorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SpecializationModel> GetMentorSpecializations(string mentorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ActivityModel> GetUserActivities(string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ConversationModel> GetUserConversations(string username)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUserLogin(UserModel model, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUserProfile(UserModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
