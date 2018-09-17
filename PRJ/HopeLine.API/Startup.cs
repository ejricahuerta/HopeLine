using HopeLine.API.Hubs;
using HopeLine.DataAccess.Interfaces;
using HopeLine.DataAccess.Repositories;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Services;
using HopeLine.Service.Configurations;
using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HopeLine.API
{
    public class
        Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServiceExtension.AddConfiguration(services);


            services.AddCors();
            services.AddLogging();
            services.AddSignalR();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            app.UseStatusCodePages(async context =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(
                        "Status code page, status code: " +
                        context.HttpContext.Response.StatusCode);
                });

            app.UseCors(opt =>
                opt.AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseMvc();

            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chat");
            });


            ConfigureServiceExtension.UseConfiguration(app);


        }
    }
}
