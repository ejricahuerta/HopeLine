using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using HopeLine.API.Hubs;
using HopeLine.Infrastructure;
using HopeLine.Infrastructure.Services;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Services;
using HopeLine.Service.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HopeLine.API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) { //JWT Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear ();

            services.AddAuthentication (opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (
                config => {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters {
                        ValidIssuer = APIConstant.URL,
                        ValidAudience = APIConstant.URL,
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (APIConstant.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                    config.Events = new JwtBearerEvents {

                        //Letting the client know that token is expired
                        //further validation needs for token on client side
                        OnAuthenticationFailed = context => {
                            if (context.Exception.GetType () == typeof (SecurityTokenExpiredException)) {
                                context.Response.Headers.Add ("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            ConfigureServiceExtension.AddConfiguration (services);

            services.AddTransient<IEmailSender, EmailSender> (i =>
                new EmailSender (
                    EmailConstants.host,
                    EmailConstants.port,
                    EmailConstants.enableSSL,
                    EmailConstants.userName,
                    EmailConstants.password
                ));

            services.AddTransient<ITokenService, TokenService> ();

            services.AddLogging ();

            services.AddCors ();

            services.AddSignalR ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }
            ConfigureServiceExtension.UseConfiguration (app);

            app.UseStatusCodePages (async context => {
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync (
                    "Status code page, status code: " +
                    context.HttpContext.Response.StatusCode);
            });

            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication ();

            app.UseCors (opt => opt.AllowAnyMethod ()
                .AllowAnyHeader ()
                .AllowAnyOrigin ()
                .AllowCredentials ());

            app.UseSignalR (route => {
                route.MapHub<ChatHub> ("/chatHub");
                route.MapHub<HopeLine.API.Hubs.v2.ChatHub> ("/v2/chatHub");
            });

            app.UseMvc ();
        }
    }
}