using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMailService, NullMailService>();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapRazorPages();

                cfg.MapControllerRoute("Default",
                    "/{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" });
            });


        }
      }
}
