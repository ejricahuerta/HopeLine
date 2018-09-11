using HopeLine.DataAccess.DatabaseContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
                using (HopeLineDbContext context = scope.ServiceProvider.GetRequiredService<HopeLineDbContext>())
                {


                }
            }
        }
    }
}