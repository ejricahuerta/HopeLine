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
        private readonly IRepository<HopeLineUser> _userRepo;
        private readonly IRepository<Conversation> _convoRepo;

        public UserService(IRepository<HopeLineUser> userRepo, IRepository<Conversation> convoRepo)
        {
            _userRepo = userRepo;
            convoRepo = convoRepo;
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            try
            {
                return _userRepo.GetAll().Select(u =>

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
                return _userRepo.GetAll()
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
            var activities = (_userRepo.Get(mentorId) as MentorAccount)
               .Activities
                .Select(n => new ActivityModel
                {
                    DateOfActivity = n.DateAdded.ToShortDateString(),
                    Description = n.Description
                });
            return activities;
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
                var conversations = (_userRepo.Get(mentorId) as MentorAccount)
                    .Conversations.Select(c => new ConversationModel
                    {
                        Id = c.Id,
                        MentorId = c.Mentor.Id,
                        UserName = c.UserName,
                        DateOfConversation = c.DateOfConversation,
                        Minutes = c.Minutes,
                        PIN = c.PIN

                    });
                return conversations;
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
            try
            {
                var schedules = (_userRepo.Get(mentorId) as MentorAccount)
                    .Schedules
                    .Select(s => new ScheduleModel
                    {


                        // TODO :  needed to be refactored
                        Id = s.Id,
                        StarTime = s.StarTime,
                        StartPeriod = s.StartPeriod,
                        EndPeriod = s.EndPeriod,
                        EndTime = s.EndTime,
                        LogoutTime = s.LogoutTime,
                        TotalHours = s.TotalHours
                    });
                return schedules;
            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to Process User Service: ", ex);
            }

        }

        public IEnumerable<SpecializationModel> GetMentorSpecializations(string mentorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ActivityModel> GetUserActivities(string userId)
        {
            try
            {
            var activities = (_userRepo.Get(userId) as UserAccount)
               .Activities
                .Select(n => new ActivityModel
                {
                    DateOfActivity = n.DateAdded.ToShortDateString(),
                    Description = n.Description
                });
            return activities;
                
            }
            catch (System.Exception ex)
            {
                
                throw new System.Exception("Unable to Process UserService", ex);
            }
        }

        public IEnumerable<ConversationModel> GetUserConversations(string username)
        {
            return _convoRepo.GetAll()
                            .Where(u=> u.UserName == username)
                            .Select(c=> new ConversationModel {
                                Id  = c.Id,
                                UserName = c.UserName,
                                DateOfConversation = c.DateOfConversation,
                                PIN = c.PIN,
                                MentorId = c.Mentor.Id,
                                Minutes = c.Minutes
                            } );
        }

        public bool UpdateUserProfile(UserModel model)
        {
        try
        {
            var user = _userRepo.Get(model.Id);
            if(model.Username != null
                && model.FirstName != null 
                && model.LastName != null && user != null) {
                    
                }
        }
        catch (System.Exception)
        {
            
            throw;
        }
        }
    }
}
