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

        public bool AddResources(ResourceModel resource)
        {
            throw new System.NotImplementedException();
        }

        public bool AddResources(CommunityModel resource)
        {
            throw new System.NotImplementedException();
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