using HopeLine.Service.Models;
using System.Collections.Generic;
using static HopeLine.DataAccess.Entities.HopeLineUser;

namespace HopeLine.Service.Interfaces
{
    public interface IUserService
    {

        IEnumerable<UserModel> GetAllUsers();
        IEnumerable<UserModel> GetAllUsersByAccountType(Account accountType);

        #region can be merged
        IEnumerable<ActivityModel> GetUserActivities(string userId);
        IEnumerable<ActivityModel> GetMentorActivities(string mentorId);
        IEnumerable<ConversationModel> GetUserConversations(string userId);
        IEnumerable<ConversationModel> GetMentorConversations(string mentorId);
        #endregion

        bool UpdateUserProfile(UserModel model);
        bool UpdateUserLogin(UserModel model, string password);
    }
}
