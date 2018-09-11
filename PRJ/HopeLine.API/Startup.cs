using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HopeLine.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<HopeLineDbContext>(opt =>
                opt.UseSqlServer("Server=tcp:prj.database.windows.net,1433;Initial Catalog=HopeLineDB;Persist Security Info=False;User ID=hopeline;Password=Prjgroup7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddIdentity<HopeLineUser, IdentityRole>(opt => { })
                .AddEntityFrameworkStores<HopeLineDbContext>()
                .AddDefaultTokenProviders();
            //TODO : Add JWT Config
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = "http://localhost:44340", //TODO : move to some class
                        ValidAudience = "http://localhost:44340", //TODO : move to some class
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somesecretkey")) // TODO : use appsettings
                    }
                );
            services.AddCors();





            //TODO : Add Dependency Injections
            services.AddTransient<ITokenService, TokenService>();
            //services.AddTransient<interface,implementationClass>();

            //TODO : Add SignalR



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
