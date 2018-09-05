
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO : move to azure keys
            optionsBuilder.UseSqlServer("Server=tcp:prj.database.windows.net,1433;Initial Catalog=HopeLineDB;Persist Security Info=False;User ID=hopeline;Password=prjgroup7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        // TODO : Add all entities
        //public Dbset<> MyProperty { get; set; }
    }
}
