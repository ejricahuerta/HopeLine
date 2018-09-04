using HopeLine.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeLine.Service.Interfaces
{
    public interface IResources
    {
        IEnumerable<Community> GetCommunities();
        IEnumerable<Resource> GetResources();

        Map DefaultMap();
    }
}
