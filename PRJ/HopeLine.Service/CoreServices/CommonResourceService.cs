using System.Collections.Generic;
using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;

namespace HopeLine.Service.CoreServices
{
    //TODO : implement interface

    /// <summary>
    /// 
    /// </summary>
    public class CommonResourceService : ICommonResource
    {
        private readonly IRepository<Resource> _resourceRepo;
        private readonly IRepository<Community> _communityRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceRepo"></param>
        /// <param name="communityRepo"></param>
        public CommonResourceService(IRepository<Resource> resourceRepo,
                                    IRepository<Community> communityRepo)
        {
            _resourceRepo = resourceRepo;
            _communityRepo = communityRepo;

        }

        //var conversations = (_userRepo.Get(mentorId) as MentorAccount)
        //           .Conversations.Select(c => new ConversationModel
        //           {
        //               Id = c.Id,
        //               MentorId = c.Mentor.Id,
        //               UserName = c.UserName,
        //               DateOfConversation = c.DateOfConversation,
        //               Minutes = c.Minutes,
        //               PIN = c.PIN

        //           });
        //        return conversations;

        // try
        //{
        //    var conversations = (_userRepo.Get(mentorId) as MentorAccount)
        //        .Conversations.Select(c => new ConversationModel
        //        {
        //            Id = c.Id,
        //            MentorId = c.Mentor.Id,
        //            UserName = c.UserName,
        //            DateOfConversation = c.DateOfConversation,
        //            Minutes = c.Minutes,
        //            PIN = c.PIN

        //        });
        //    return conversations;
        //}
        //catch (System.Exception ex)
        //{

        //    throw new System.Exception("Unable to process user service : ", ex);
        //}


        public bool AddResources(ResourceModel resource)
        {
            try
            {
                var _resource = new Resource
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Url = resource.Url
                };
                _resourceRepo.Insert(_resource);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool AddResources(CommunityModel resource)
        {
            try
            {
                var _resource = new Community
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    URL = resource.URL,
                    ImageURL = resource.ImageURL
                };
                _communityRepo.Insert(_resource);
                return true;
            }
            catch (System.Exception e)
            {
                // TODO : Log error
                System.Console.WriteLine("Error: " + e);
                return false;
            }
        }

        public Map DefaultMap()
        {
            throw new System.NotImplementedException();
        }

        public bool EditDefaultMap(MapModel map)
        {
            throw new System.NotImplementedException();
        }

        public bool EditResource(ResourceModel resource)
        {
            throw new System.NotImplementedException();
        }

        public bool EditResource(CommunityModel resource)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Community> GetCommunities()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<LanguageModel> GetLanguages()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Resource> GetResources()
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveCommunity(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveResource(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}