using System.Collections.Generic;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Models;

namespace HopeLine.Service.Interfaces {
    /// <summary>
    /// 
    /// </summary>
    public interface ICommonResource {
        IEnumerable<CommunityModel> GetCommunities ();
        IEnumerable<ResourceModel> GetResources ();
        IEnumerable<LanguageModel> GetLanguages ();
        IEnumerable<TopicModel> GetTopics ();

        bool AddResources (ResourceModel resource);
        bool EditResource (ResourceModel resource);
        bool AddCommunity (CommunityModel resource);
        bool EditCommunity (CommunityModel resource);
        bool AddTopics (TopicModel topic);
        bool AddLanguage (LanguageModel language);

        bool RemoveResource (int id);
        bool RemoveCommunity (int id);

        void SaveTopic ();
        void SaveResource ();
        void SaveCommunity ();
        void SaveLanguage ();
        Task SaveTopicAsync ();
        Task SaveResourceAsync ();
        Task SaveCommunityAsync ();
        Task SaveLanguageAsync ();
    }
}