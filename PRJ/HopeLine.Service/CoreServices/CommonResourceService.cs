using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace HopeLine.Service.CoreServices
{
    //TODO : implement interface

    /// <summary>
    /// 
    /// </summary>
    public class CommonResourceService : ICommonResource
    {
        private readonly IRepository<Language> _languageRepo;
        private readonly IRepository<Resource> _resourceRepo;
        private readonly IRepository<Community> _communityRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceRepo"></param>
        /// <param name="communityRepo"></param>
        public CommonResourceService(IRepository<Resource> resourceRepo,
                                    IRepository<Community> communityRepo,
                                    IRepository<Language> languageRepo)
        {
            _languageRepo = languageRepo;
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
                    URL = resource.URL
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
            // Default location is Seneca College
            var map = new Map
            {
                XCoordinate = 43.771539,
                YCoordinate = -79.498708,
                Radius = 50
            };
            return map;
        }

        public bool EditDefaultMap(MapModel map)
        {
            try
            {
                var _map = new Map
                {
                    XCoordinate = map.XCoordinate,
                    YCoordinate = map.YCoordinate,
                    Radius = map.Radius
                };
                return true;
            }
            catch (System.Exception e)
            {
                // TODO : Log error
                System.Console.WriteLine("Error: " + e);
                return false;
            }
        }

        public bool EditResource(ResourceModel resource)
        {
            try
            {
                var _resource = new Resource
                {
                    Name = resource.Name,
                    Description = resource.Description,
                    ImageURL = resource.ImageURL,
                    URL = resource.URL
                };
                _resourceRepo.Update(_resource);
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error: " + e);
                return false;
            }
        }

        public bool EditResource(CommunityModel resource)
        {
            try
            {
                var _resource = new Community
                {
                    Name = resource.Name,
                    Description = resource.Description,
                    URL = resource.URL,
                    ImageURL = resource.ImageURL
                };
                _communityRepo.Update(_resource);
                return true;
            }
            catch (System.Exception e)
            {
                // TODO: Error
                System.Console.WriteLine("Error:" + e);
                return false;
            }
        }

        public IEnumerable<CommunityModel> GetCommunities()
        {
            return (_communityRepo as IEnumerable<Community>).Select(c => new CommunityModel {
                Name = c.Name,
                Description = c.Description,
                URL = c.URL,
                ImageURL = c.ImageURL
            });
        }

        public IEnumerable<LanguageModel> GetLanguages()
        {
            return (_languageRepo as IEnumerable<Language>).Select(c => new LanguageModel
            {
                Name = c.Name,
                CountryOrigin = c.CountryOrigin,
                ProfileLanguages = c.ProfileLanguages
            });
        }
        //@Edmel, I changed the return from Resource to ResourceModel. Tell me if that is what you wanted
        public IEnumerable<ResourceModel> GetResources()
        {
            return (_resourceRepo as IEnumerable<Resource>).Select(c => new ResourceModel
            {
                Name = c.Name,
                Description = c.Description,
                URL = c.URL,
                ImageURL = c.ImageURL
            });
        }

        public bool RemoveCommunity(int id)
        {
            try
            {
                _communityRepo.Remove(id);
                return true;
            }catch(System.Exception e)
            {
                System.Console.WriteLine("Error: " + e);
                return false;
            }
        }

        public bool RemoveResource(int id)
        {
            try
            {
                _resourceRepo.Remove(id);
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error: " + e);
                return false;
            }
        }
    }
}