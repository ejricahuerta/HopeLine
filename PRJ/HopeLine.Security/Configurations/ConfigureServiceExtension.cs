using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HopeLine.Security.Configurations
{
    public class ConfigureServiceExtension
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddDbContext<HopeLineDbContext>(opt =>
                    opt.UseSqlServer("Server=tcp:prj.database.windows.net,1433;Initial Catalog=HopeLineDB;Persist Security Info=False;User ID=hopeline;Password=Prjgroup7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("longsomesecretkey")) // TODO : use appsettings
                    }
                );
        }
    }
}
