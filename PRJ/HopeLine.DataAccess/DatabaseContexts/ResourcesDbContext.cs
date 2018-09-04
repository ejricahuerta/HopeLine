
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeLine.DataAccess.DatabaseContexts
{

    //TODO : Add Community and Resources
    class ResourcesDbContext : IdentityDbContext<HopeLineUser>
    {

        //TODO : Add all web resources /  components model
        public DbSet<Map> Maps { get; set; }

        public DbSet<Community> Communities { get; set; }

        public DbSet<Resource> Resources { get; set; }


        public ResourcesDbContext()
        {

        }
    }
}
