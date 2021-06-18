using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI
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
            services.AddControllersWithViews();
            services.AddDbContext<KolayIkContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KolayIkConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "SirketYonetici",
                     areaName:"SirketYonetici",
                    pattern: "SirketYonetici/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                 name: "SiteYonetici",
                  areaName: "SiteYonetici",
                 pattern: "SiteYonetici/{controller=Home}/{action=Index}/{id?}");



                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Ziyaretci}/{action=Index}");
            });

        

        }
    }
}
