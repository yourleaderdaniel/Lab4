using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyAspNetCoreProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); 
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); 
                app.UseHsts(); 
            }

            app.UseHttpsRedirection(); 
            app.UseStaticFiles(); 

            app.UseRouting(); 

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "library",
                    pattern: "Library",
                    defaults: new { controller = "Library", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "books",
                    pattern: "Library/Books",
                    defaults: new { controller = "Library", action = "Books" });

                endpoints.MapControllerRoute(
                    name: "profile",
                    pattern: "Library/Profile/{id?}",
                    defaults: new { controller = "Library", action = "Profile" },
                    constraints: new { id = @"\d{0,5}" }); 
            });
        }
    }
}
