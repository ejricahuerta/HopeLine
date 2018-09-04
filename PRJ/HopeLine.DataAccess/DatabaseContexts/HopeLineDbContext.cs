
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeLine.DataAccess.DatabaseContexts
{

    //TODO : Add References to Identity
    public class HopeLineDbContext : IdentityDbContext<HopeLineUser>
    {
        public HopeLineDbContext()
        {

        }

        // TODO : Add all entities
        //public Dbset<> MyProperty { get; set; }

    }
}
