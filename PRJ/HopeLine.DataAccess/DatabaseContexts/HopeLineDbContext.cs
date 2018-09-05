
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HopeLine.DataAccess.DatabaseContexts
{

    //TODO : Add References to Identity
    public class HopeLineDbContext : IdentityDbContext<HopeLineUser>
    {
        public HopeLineDbContext()
        {

        }

        public HopeLineDbContext(DbContextOptions<HopeLineDbContext> options) : base(options)
        {
        }

        // TODO : Add all entities
        //public Dbset<> MyProperty { get; set; }

    }
}
