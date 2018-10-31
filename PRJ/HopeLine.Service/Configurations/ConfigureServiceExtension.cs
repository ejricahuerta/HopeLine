using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.DataAccess.Entities.Base;
using HopeLine.DataAccess.Interfaces;
using HopeLine.DataAccess.Repositories;
using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace HopeLine.Service.Configurations
{
    public class ConfigureServiceExtension
    {

        /// <summary>
        /// Extended middleware config to separate classes.
        /// </summary>
        /// <param name="services"></param>
        public static void AddConfiguration(IServiceCollection services)
        {

            services.AddDbContext<ChatDbContext>(opt =>
                            opt.UseInMemoryDatabase("chatdb"));

            services.AddDbContext<ResourcesDbContext>(opt => opt
                    .UseMySql(APIConstant.ConnectionString,
                        mysqlOptions =>
                        {
                            mysqlOptions
                                .ServerVersion(new Version(3, 23), ServerType.MySql);
                        }));
            //.UseInMemoryDatabase("chatdb"));
            // .UseSqlServer(APIConstant.ConnectionString));
            services.AddDbContext<HopeLineDbContext>(opt => opt
                                                    .UseMySql(APIConstant.ConnectionString,
                        mysqlOptions =>
                        {
                            mysqlOptions
                                .ServerVersion(new Version(3, 23), ServerType.MySql);
                        }));
            //.UseInMemoryDatabase("chatdb"));
            //.UseSqlServer(APIConstant.ConnectionString));
            services.AddIdentity<HopeLineUser, IdentityRole>()
                .AddEntityFrameworkStores<HopeLineDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.User.RequireUniqueEmail = true;
            });

            //all interface and implementation
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IRepository<HopeLineUser>, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommunication, CommunicationService>();
            services.AddTransient<IMessage, MessageService>();
            services.AddTransient<ICommonResource, CommonResourceService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Add extenstion here 
        /// </summary>
        /// <param name="app"></param>
        public static void UseConfiguration(IApplicationBuilder app)
        {
            // implement additional config when the app runs HERE
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<HopeLineDbContext>())
                    context.Database.EnsureCreated();

                //TODO : do populate data HERE!
            }
        }
    }
}


