using HopeLine.DataAccess.Entities;
using System.Collections.Generic;

namespace HopeLine.Service.Interfaces
{
    public interface IResources
    {
        IEnumerable<Community> GetCommunities();
        IEnumerable<Resource> GetResources();

        Map DefaultMap();
    }
}
