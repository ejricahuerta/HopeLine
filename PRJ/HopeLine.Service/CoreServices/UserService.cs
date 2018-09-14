using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace HopeLine.Service.CoreServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<HopeLineUser> _repository;

        public UserService(IRepository<HopeLineUser> repository)
        {
            _repository = repository;
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            try
            {
                return _repository.GetAll().Select(u =>

                     new UserModel
                     {
                         Id = u.Id,
                         FirstName = (u.Profile != null) ? u.Profile.FirstName : "",
                         LastName = (u.Profile != null) ? u.Profile.LastName : "",
                         Languages = new List<string>(),
                         AccountType = u.AccountType.ToString(),
                         Username = u.UserName,
                         Email = u.Email
                     });

            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to process Service :", ex);
            }
        }

        public IEnumerable<UserModel> GetAllUsersByAccountType(string userType)
        {
            try
            {
                return _repository.GetAll()
                    .Where(a => a.AccountType.ToString().Contains(userType))
                    .Select(u =>
                           new UserModel
                           {
                               Id = u.Id,

                               FirstName = (u.Profile != null) ? u.Profile.FirstName : "",
                               LastName = (u.Profile != null) ? u.Profile.LastName : "",
                               Languages = new List<string>(),
                               AccountType = u.AccountType.ToString(),
                               Username = u.UserName,
                               Email = u.Email
                           });
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to process Service :", ex);
            }

        }
        /// <summary>
        /// This function gets all mentors Activities
        /// </summary>
        /// <param name="mentorId"></param>
        /// <returns> all activities of mentor </returns>
        public IEnumerable<ActivityModel> GetMentorActivities(string mentorId)
        {
            var all = (_repository.GetAll().Where(u => u.AccountType == HopeLineUser.Account.Mentor && u.Id == mentorId) as MentorAccount)
                .Activities
                .Select(n => new ActivityModel
                {
                    DateOfActivity = n.DateAdded.ToShortDateString(),
                    Description = n.Description
                });
            return all;
        }

        /// <summary>
        /// This function returns a collection of conversationmodel class from repo
        /// </summary>
        /// <param name="mentorId"></param>
        /// <returns>all mentors conversations</returns>
        public IEnumerable<ConversationModel> GetMentorConversations(string mentorId)
        {
            try
            {
                var mentors = _repository.GetAll().Where(u => u.Id == mentorId && u.AccountType == HopeLineUser.Account.Mentor);
                return (mentors as MentorAccount).Conversations.Select(c => new ConversationModel
                {
                    Id = c.Id,
                    MentorId = c.Mentor.Id,
                    UserId = c.UserId,
                    UserName = c.UserName,
                    DateOfConversation = c.DateOfConversation,
                    Minutes = c.Minutes,
                    PIN = c.PIN

                });
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to process user service : ", ex);
            }

        }


        /// <summary>
        /// This function returns collection of schedule for mentor from repo
        /// </summary>
        /// <param name="mentorId"></param>
        /// <returns>IEnumerable<ScheduleModel></returns>
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
