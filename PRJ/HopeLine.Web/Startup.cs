using System;
using HopeLine.Infrastructure.Services;

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
using Microsoft.AspNetCore.Identity.UI.Services;
using HopeLine.Infrastructure;

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

            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    EmailConstants.host,
                    EmailConstants.port,
                    EmailConstants.enableSSL,
                    EmailConstants.userName,
                    EmailConstants.password
                ));

            services.ConfigureApplicationCookie(options =>
               {
                   options.Cookie.HttpOnly = true;
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(120);

                   options.LoginPath = "/Index";
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
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
        
                app.UseStatusCodePages(async context =>
                {
                    
                    context.HttpContext.Response.ContentType = "text/plain";
                    
                    await context.HttpContext.Response.WriteAsync(
                        "Status code page, status code: " +
                        context.HttpContext.Response.StatusCode + " " + context.HttpContext.Response.ContentType);
                });
                app.UseStatusCodePagesWithRedirects("/error/{0}");
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


            app.UseCors(opt => opt.AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowAnyOrigin()
                                .AllowCredentials());

            app.UseMvc();

            ConfigureServiceExtension.UseConfiguration(app);
        }
    }
}
