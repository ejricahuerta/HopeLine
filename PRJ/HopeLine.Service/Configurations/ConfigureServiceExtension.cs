using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddDbContext<HopeLineDbContext>(opt => opt
                                                .UseSqlServer(APIConstant.ConnectionString));
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
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });

            //JWT Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = APIConstant.URL,
                        ValidAudience = APIConstant.URL,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretofGroup")),
                        ClockSkew = TimeSpan.Zero
                    };
                    config.Events = new JwtBearerEvents
                    {

                        //Letting the client know that token is expired
                        //further validation needs for token on client side
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };

                });


        }

        /// <summary>
        /// Add extenstion here 
        /// </summary>
        /// <param name="app"></param>
        public static void UseConfiguration(IApplicationBuilder app)
        {
            //implement additional config when the app runs HERE
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<HopeLineDbContext>())
                    context.Database.EnsureCreated();

                //TODO : do populate data HERE!
            }
        }
    }
}


