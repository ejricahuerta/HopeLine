using HopeLine.DataAccess.Entities;
using HopeLine.Service.Models;
using System.Collections.Generic;

namespace HopeLine.Service.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommonResource
    {
        IEnumerable<CommunityModel> GetCommunities();
        IEnumerable<ResourceModel> GetResources();
        IEnumerable<LanguageModel> GetLanguages();
        IEnumerable<TopicModel> GetTopics();
        
        Map DefaultMap();
        bool EditDefaultMap(MapModel map);
        bool AddResources(ResourceModel resource);
        bool EditResource(ResourceModel resource);
        bool AddCommunity(CommunityModel resource);
        bool EditCommunity(CommunityModel resource);

        bool RemoveResource(int id);
        bool RemoveCommunity(int id);

    }
}
