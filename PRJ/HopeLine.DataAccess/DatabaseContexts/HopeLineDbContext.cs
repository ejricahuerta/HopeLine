
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HopeLine.DataAccess.DatabaseContexts
{

    //TODO : Add References to Identity

    /// <summary>
    /// 
    /// </summary>
    public class HopeLineDbContext : IdentityDbContext<HopeLineUser>
    {
        public HopeLineDbContext()
        {

        }

        // TODO : Add all entities
        //public Dbset<> MyProperty { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Topic> Topics { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public HopeLineDbContext(DbContextOptions<HopeLineDbContext> options) : base(options)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO : move to azure keys
            optionsBuilder.UseSqlServer("Server=tcp:prj.database.windows.net,1433;Initial Catalog=HopeLineDB;Persist Security Info=False;User ID=hopeline;Password=Prjgroup7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}
