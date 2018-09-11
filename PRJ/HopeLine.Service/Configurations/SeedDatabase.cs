using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HopeLine.Service.Configurations
{
    public static class SeedDatabase
    {
        public static void Initialize(this IApplicationBuilder builder)
        {
            var provider = builder.ApplicationServices;
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<HopeLineDbContext>())
                {

                    context.Database.EnsureCreated();
                    if (!context.Users.Any())
                    {
                        UserAccount user = new UserAccount
                        {
                            Email = "exricahuerta@gmail.com",
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = "exricahuerta@gmail.com",
                        };
                        using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HopeLineUser>>())
                        {
                            userManager.CreateAsync(user, "password");
                        }
                    }

                }
            }
        }
    }
}