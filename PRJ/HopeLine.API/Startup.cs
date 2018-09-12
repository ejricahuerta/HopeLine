using HopeLine.DataAccess.DatabaseContexts;
using HopeLine.Service.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            ConfigureServiceExtension.AddConfiguration(services);


            services.AddCors();
            services.AddLogging();
            services.AddMvc();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                HopeLineDbContext context // TODO : this shouldn't be be here
                                )
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
            app.UseMvc();


            //TODO : create a static class to access this from service layer instead
            context.Database.EnsureCreated();
        }
    }
}
