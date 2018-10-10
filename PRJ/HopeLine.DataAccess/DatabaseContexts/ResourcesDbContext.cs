
using System;
using HopeLine.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace HopeLine.DataAccess.DatabaseContexts
{

    //TODO : Add Community and Resources
    public class ResourcesDbContext : DbContext
    {

        //TODO : Add all web resources /  components model
        public DbSet<Map> Maps { get; set; }

        public DbSet<Community> Communities { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public ResourcesDbContext()
        {

        }

        public ResourcesDbContext(DbContextOptions<ResourcesDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO : move to appsettings.json file
            optionsBuilder.UseMySql("server=zenit.senecac.on.ca;database=prj566_182a07;user=prj566_182a07;password=hfAJ9737",
            mysqlOptions =>
        {
            mysqlOptions
                .ServerVersion(new Version(3, 23), ServerType.MySql);
        });
            //.UseSqlServer("Server=tcp:prj.database.windows.net,1433;Initial Catalog=HopeLineDB;Persist Security Info=False;User ID=hopeline;Password=Prjgroup7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
