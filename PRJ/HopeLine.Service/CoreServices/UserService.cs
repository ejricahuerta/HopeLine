using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace HopeLine.Service.CoreServices
{

    /// <summary>
    /// This class is all user related functionalities and services
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<HopeLineUser> _userRepo;
        private readonly IRepository<Conversation> _convoRepo;
        private readonly IRepository<MentorSpecialization> _specializationRepo;

        public UserService(IUnitOfWork unitOfWork, IRepository<HopeLineUser> userRepo,
                            IRepository<Conversation> convoRepo,
                            IRepository<MentorSpecialization> specializationRepo)
        {
            _unitOfWork = unitOfWork;
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
                //if any error, throw this
                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
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

                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
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
                var activities = (_userRepo.Get(mentorId) as MentorAccount)
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
                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
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

                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
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
                var schedules = (_userRepo.Get(mentorId) as MentorAccount)
                    .Schedules
                    .Select(s => new ScheduleModel
                    {
                        //

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
            try
            {
                var specializations = _specializationRepo.GetAll("Specialization").Where(m => m.MentorAccountId == mentorId).Select(s => new SpecializationModel
                {
                    Id = s.SpecializationId,
                    Description = s.Specialization.Description,
                    Name = s.Specialization.Name
                });

                return specializations;
            }
            catch (System.Exception ex)
            {
                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
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
                        DateOfActivity = n.DateAdded.ToShortDateString(),
                        Description = n.Description
                    });
                return activities;

            }
            catch (System.Exception ex)
            {
                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
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

                return _convoRepo.GetAll()
                                .Where(u => u.UserName == username)
                                .Select(c => new ConversationModel
                                {
                                    Id = c.Id,
                                    UserName = c.UserName,
                                    DateOfConversation = c.DateOfConversation,
                                    PIN = c.PIN,
                                    MentorId = c.Mentor.Id,
                                    Minutes = c.Minutes
                                });
            }
            catch (System.Exception ex)
            {
                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
                return null;
            }
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
                    _unitOfWork.SaveAsync();
                }
                return true;
            }
            catch (System.Exception ex)
            {

                //throw new System.Exception("Unable to process Service :", ex);
                System.Console.WriteLine("Unable to process Service :", ex);
                return false;
            }
        }
    }
}