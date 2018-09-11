using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.Security.Configurations;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Services;
using HopeLine.Service.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            ConfigureServiceExtension.Configure(services);
            services.AddLogging();
            services.AddCors();

            //TODO : Add Dependency Injections
            services.AddTransient<ITokenService, TokenService>();
            //services.AddTransient<interface,implementationClass>();

            //TODO : Add SignalR



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                HopeLineDbContext context,
                                UserManager<HopeLineUser> manager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(opt =>
            opt.AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            SeedDatabase.Initialize(app);

        }
    }
}
