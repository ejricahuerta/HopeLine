
using System;
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
        #region all accounts
        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<MentorAccount> MentorAccounts { get; set; }
        public DbSet<GuestAccount> GuestAccounts { get; set; }
        #endregion
        // TODO : Add all entities

        //public Dbset<> MyProperty { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<MentorAccount> Mentors { get; set; }
        public DbSet<AdminAccount> Admins { get; set; }
        public DbSet<UserAccount> RegisteredUsers { get; set; }
        public DbSet<GuestAccount> Guests { get; set; }
        public DbSet<ProfileLanguage> ProfileLanguages { get; set; }
        public DbSet<MentorSpecialization> MentorSpecializations { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<Community> Communities { get; set; }

        public DbSet<Resource> Resources { get; set; }
        /// <summary>
        /// Override constructor with options
        /// </summary>
        /// <param name="options"></param>
        public HopeLineDbContext(DbContextOptions<HopeLineDbContext> options) : base(options)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     //TODO : move to appsettings.json file
        //     optionsBuilder.UseMySql("server=zenit.senecac.on.ca;database=prj566_182a07;user=prj566_182a07;password=hfAJ9737",
        //     mysqlOptions =>
        // {
        //     mysqlOptions
        //         .ServerVersion(new Version(3, 23), ServerType.MySql);
        // });
        //     //.UseSqlServer("Server=tcp:prj.database.windows.net,1433;Initial Catalog=HopeLineDB;Persist Security Info=False;User ID=hopeline;Password=Prjgroup7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //this is a temporary fix EF team is removing all many to many relationship soon
            //https://github.com/aspnet/EntityFrameworkCore/issues/1368


            #region profile and language many to many relationship
            modelBuilder.Entity<ProfileLanguage>()
                .HasKey(k => new { k.ProfileId, k.LanguageId });

            modelBuilder.Entity<ProfileLanguage>()
                .HasOne(p => p.Profile)
                .WithMany(l => l.ProfileLanguages)
                .HasForeignKey(pl => pl.ProfileId);

            modelBuilder.Entity<ProfileLanguage>()
                .HasOne(p => p.Language)
                .WithMany(l => l.ProfileLanguages)
                .HasForeignKey(pl => pl.LanguageId);
            #endregion


            #region mentor and specialization many to many

            modelBuilder.Entity<MentorSpecialization>().
                HasKey(k => new { k.MentorAccountId, k.SpecializationId });

            modelBuilder.Entity<MentorSpecialization>()
                .HasOne(m => m.MentorAccount)
                .WithMany(ms => ms.MentorSpecializations)
                .HasForeignKey(m => m.MentorAccountId);

            modelBuilder.Entity<MentorSpecialization>()
                .HasOne(s => s.Specialization)
                .WithMany(ms => ms.MentorSpecializations).
                HasForeignKey(s => s.SpecializationId);

            #endregion
        }
    }
}
