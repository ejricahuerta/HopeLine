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
        IEnumerable<Community> GetCommunities();
        IEnumerable<Resource> GetResources();
        IEnumerable<LanguageModel> GetLanguages();
        Map DefaultMap();
        bool EditDefaultMap(MapModel map);
        bool AddResources(ResourceModel resource);
        bool EditResource(ResourceModel resource);
        bool AddResources(CommunityModel resource);
        bool EditResource(CommunityModel resource);

        bool RemoveResource(int id);

        bool RemoveCommunity(int id);

    }
}
