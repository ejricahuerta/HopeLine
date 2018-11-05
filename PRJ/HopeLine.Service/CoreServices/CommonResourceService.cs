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
        private readonly IRepository<Topic> _topicRepo;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceRepo"></param>
        /// <param name="communityRepo"></param>
        public CommonResourceService(IRepository<Resource> resourceRepo,
                                    IRepository<Community> communityRepo,
                                    IRepository<Language> languageRepo,
                                    IRepository<Topic> topicRepo)
        {
            _languageRepo = languageRepo;
            _resourceRepo = resourceRepo;
            _communityRepo = communityRepo;
            _topicRepo = topicRepo;
        }

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

        public bool AddCommunity(CommunityModel resource)
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

        public bool EditCommunity(CommunityModel resource)
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
            try
            {
                return _communityRepo.GetAll(null).Select(c => new CommunityModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    URL = c.URL,
                    ImageURL = c.ImageURL
                });
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to fetch Communites", ex);
            }
        }

        public IEnumerable<LanguageModel> GetLanguages()
        {
            return _languageRepo.GetAll(null).Select(c => new LanguageModel
            {
                Name = c.Name,
                CountryOrigin = c.CountryOrigin,
                ProfileLanguages = c.ProfileLanguages
            });
        }
        //@Edmel, I changed the return from Resource to ResourceModel. Tell me if that is what you wanted
        public IEnumerable<ResourceModel> GetResources()
        {
            try
            {
                return _resourceRepo.GetAll(null).Select(c => new ResourceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    URL = c.URL,
                    ImageURL = c.ImageURL
                });

            }
            catch (System.Exception ex)
            {

                throw new System.Exception("Unable to Fetch from Service", ex);
            }
        }

        public bool RemoveCommunity(int id)
        {
            try
            {
                _communityRepo.Remove(id);
                return true;
            }
            catch (System.Exception e)
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

        public IEnumerable<TopicModel> GetTopics()
        {
            try
            {
                return _topicRepo.GetAll(null).Select(t => new TopicModel
                {
                    Name = t.Name,
                    Id = t.Id,
                    Description = t.Description
                });
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to Fetch Data:", ex);
            }
        }
    }
}