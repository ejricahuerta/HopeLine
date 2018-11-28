using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Interfaces;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CommonResourceService> _logger;
        private readonly IRepository<Resource> _resourceRepo;
        private readonly IRepository<Community> _communityRepo;
        private readonly IRepository<Topic> _topicRepo;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceRepo"></param>
        /// <param name="communityRepo"></param>
        public CommonResourceService(ILogger<CommonResourceService> logger, IRepository<Resource> resourceRepo,
                                    IRepository<Community> communityRepo,
                                    IRepository<Language> languageRepo,
                                    IRepository<Topic> topicRepo)
        {
            _languageRepo = languageRepo;
            _logger = logger;
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
                _logger.LogInformation("Inserting new resource -  {}", resource.Name);
                return true;
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Unable to insert {}. ", resource.Name);
                return false;
            }
        }

        public bool AddCommunity(CommunityModel resource)
        {
            try
            {
                var newresource = new Community
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    URL = resource.URL,
                    ImageURL = resource.ImageURL
                };
                _communityRepo.Insert(newresource);
                _logger.LogInformation("Inserting new community -  {}", resource.Name);
                return true;
            }
            catch (System.Exception)
            {
                // TODO : Log error
                _logger.LogWarning("Unable to insert {}. ", resource.Name);
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
                var oldresource = new Resource
                {
                    Name = resource.Name,
                    Description = resource.Description,
                    ImageURL = resource.ImageURL,
                    URL = resource.URL
                };
                _resourceRepo.Update(oldresource);
                _logger.LogInformation("Updating resource -  {}", resource.Name);
                return true;
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Unable to update {}. ", resource.Name);
                return false;
            }
        }

        public bool EditCommunity(CommunityModel resource)
        {
            try
            {
                var oldresource = new Community
                {
                    Name = resource.Name,
                    Description = resource.Description,
                    URL = resource.URL,
                    ImageURL = resource.ImageURL
                };
                _communityRepo.Update(oldresource);
                _logger.LogInformation("Updating community -  {}", resource.Name);
                return true;
            }
            catch (System.Exception)
            {
                // TODO: Error
                _logger.LogWarning("Unable to update {}. ", resource.Name);
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
                _logger.LogWarning("Unable to fetch communities : {}", ex);
            }
            return null;
        }

        public IEnumerable<LanguageModel> GetLanguages()
        {

            _logger.LogInformation("Fetching Languages");
            return _languageRepo.GetAll(null).Select(c => new LanguageModel
            {
                Name = c.Name,
                CountryOrigin = c.CountryOrigin,
                ProfileLanguages = c.ProfileLanguages
            });
        }
        public IEnumerable<ResourceModel> GetResources()
        {
            try
            {
                _logger.LogInformation("Fetching Languages");
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

                _logger.LogWarning("Unable to Fetch from Service: {}", ex);
            }
            return null;
        }

        public bool RemoveCommunity(int id)
        {
            try
            {
                _logger.LogInformation("Removing Community By ID :{}", id);
                _communityRepo.Remove(id);
                return true;
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Unable to Remove Community by ID: {}", id);
                return false;
            }
        }

        public bool RemoveResource(int id)
        {
            try
            {
                _resourceRepo.Remove(id);
                _logger.LogInformation("Removing Resource By ID :{}", id);
                return true;
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Unable to Remove Resource by ID: {}", id);
                return false;
            }
        }

        public IEnumerable<TopicModel> GetTopics()
        {
            try
            {
                _logger.LogInformation("Fetching All Topics");
                return _topicRepo.GetAll(null).Select(t => new TopicModel
                {
                    Name = t.Name,
                    Id = t.Id,
                    Description = t.Description
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Unable to Fetch Data: {}", ex);
                return null;
            }
        }
    }
}