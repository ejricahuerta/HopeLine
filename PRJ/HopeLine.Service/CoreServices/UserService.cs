using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HopeLine.Service.CoreServices
{

    /// <summary>
    /// This class is all user related functionalities and services
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRepository<HopeLineUser> _userRepo;
        private readonly IRepository<Conversation> _convoRepo;
        private readonly IRepository<MentorSpecialization> _specializationRepo;

        public UserService(ILogger<UserService> logger, IRepository<HopeLineUser> userRepo,
                            IRepository<Conversation> convoRepo,
                            IRepository<MentorSpecialization> specializationRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _convoRepo = convoRepo;
            _specializationRepo = specializationRepo;
        }

        /// <summary>
        /// This function returns a list of user model class mapped from user entities repo
        /// </summary>
        /// <returns>A list of UserModel</returns>
        public IEnumerable<UserModel> GetAllUsers()
        {
            try
            {
                // For each user return a value
                return _userRepo.GetAll()
                    .Select(u =>
                     //for each value map to user model
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
                _logger.LogWarning("Unable to process Service: {}", ex);
                return null;
            }
        }
        /// <summary>
        ///  This function returns a list of user model class mapped from user entities repo for specified user type
        /// </summary>
        /// <param name="userType"></param>
        /// <returns>a list of usermodel</returns>
        public IEnumerable<UserModel> GetAllUsersByAccountType(string userType)
        {
            try
            {
                _logger.LogInformation("Get All Users by {}", userType);
                // for each value that has property value of this function param - userType
                return _userRepo.GetAll()
                        .Where(a => a.AccountType.ToString().Contains(userType))
                        .Select(u =>
                           //for each value map to usermodel
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

                _logger.LogWarning("Unable to process Service: {}", ex);
                return null;
            }

        }
        /// <summary>
        /// This function gets all mentors Activities
        /// </summary>
        /// <param name="mentorId"></param>
        /// <returns> all activities of mentor </returns>
        public IEnumerable<ActivityModel> GetMentorActivities(string mentorId)
        {
            try
            {
                _logger.LogInformation("Get All Mentor - {} Activities", mentorId);
                var activities = (_userRepo.Get((object)mentorId) as MentorAccount)
                   .Activities
                    .Select(n => new ActivityModel
                    {
                        DateOfActivity = n.DateAdded,
                        Description = n.Description
                    });
                return activities;

            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to process Service : {", ex);
                return null;
            }
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
                        MentorId = c.MentorId,
                        UserId = c.UserId,
                        DateOfConversation = DateTime.Parse(c.DateOfConversation),
                        Minutes = c.Minutes,
                        PIN = c.PIN

                    });
                _logger.LogInformation("Get All Mentor - {} Conversations", mentorId);
                return conversations;
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to process Service : {}", ex);
                return null;
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
                var schedules = (_userRepo.Get((object)mentorId) as MentorAccount)
                    .Schedules
                    .Select(s => new ScheduleModel
                    {
                        //

                    });
                return schedules;
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to get Mentor Schedule: ", ex);
            }

        }

        public IEnumerable<SpecializationModel> GetMentorSpecializations(string mentorId)
        {
            try
            {
                var specializations = _specializationRepo.GetAll("Specialization")
                    .Where(m => m.MentorAccountId == mentorId)
                    .Select(s => new SpecializationModel
                    {
                        Id = s.SpecializationId,
                        Description = s.Specialization.Description,
                        Name = s.Specialization.Name
                    });
                _logger.LogInformation("Get Mentor - {} Specializations", mentorId);
                return specializations;
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to process Service : {}", ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<ActivityModel> GetUserActivities(string userId)
        {
            try
            {
                var activities = (_userRepo.Get(userId) as UserAccount)
                   .Activities
                    .Select(n => new ActivityModel
                    {
                        DateOfActivity = n.DateAdded,
                        Description = n.Description
                    });
                _logger.LogInformation("Get All User - {} Activities", userId);
                return activities;

            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to process Service :{} ", ex);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<ConversationModel> GetUserConversations(string username)
        {
            try
            {
                _logger.LogInformation("get User - {} Activities", username);
                return _convoRepo.GetAll()
                                .Where(u => u.UserId == username)
                                .Select(c => new ConversationModel
                                {
                                    Id = c.Id,
                                    UserId = c.UserId,
                                    DateOfConversation = DateTime.Parse(c.DateOfConversation),
                                    PIN = c.PIN,
                                    MentorId = c.MentorId,
                                    Minutes = c.Minutes
                                });
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to process Service : {}", ex);
                return null;
            }
        }

        public IEnumerable<string> ListMentorIdByAvailability(bool available)
        {
            _logger.LogTrace("Not Implemented");
            return null;
        }

        public bool UpdateUserProfile(UserModel model)
        {
            try
            {
                var user = _userRepo.Get(model.Id);
                if (model.Username != null
                    && model.FirstName != null
                    && model.LastName != null && user != null)
                {
                    user.Profile.FirstName = model.FirstName;
                    user.Profile.LastName = model.LastName;
                    _userRepo.Update(user);
                }
                _logger.LogInformation("Updating User - {} Info", model.Id);
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to process Service: {}", ex);
                return false;
            }
        }
    }
}