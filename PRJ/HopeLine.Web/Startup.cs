using System;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Services;
using HopeLine.Service.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HopeLine.Web
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
            ConfigureServiceExtension.AddConfiguration(services);

            services.ConfigureApplicationCookie(options =>
               {
                   options.Cookie.HttpOnly = true;
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(120);

                   options.LoginPath = "/Authenticate";
                   options.AccessDeniedPath = "/Account/AccessDenied";
                   options.SlidingExpiration = true;
               });

            //Register all Require Claims for auth
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("MentorOnly", policy => policy.RequireClaim("Account", "Mentor"));
                opt.AddPolicy("UserOnly", policy => policy.RequireClaim("Account", "User"));
                opt.AddPolicy("AdminOnly", policy => policy.RequireClaim("Account", "Admin"));
                opt.AddPolicy("SuperUser", policy => policy.RequireClaim("Account", "Super"));

            });

            //Session Enable for Guest User
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddSessionStateTempDataProvider();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(240);
                options.Cookie.HttpOnly = true;
            });

            //Required for accessing  hhttpcontext
            services.AddHttpContextAccessor();

            //For Web Api CORS
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                            builder =>
                            {
                                builder.WithOrigins("https://hopelineapi.azurewebsites.net/")
                                .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .DisallowCredentials();
                            }
            ));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            //Required to proxy when deployed to apache or nginx
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseCors("CorsPolicy");

            app.UseMvc();

            ConfigureServiceExtension.UseConfiguration(app);
        }
    }
}
