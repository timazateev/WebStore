using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Services;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Data;

namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {
        //private IConfiguration Configuration { get; }
        //public Startup(IConfiguration Configuration)
        //{
        //    this.Configuration = Configuration;
        //}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            services.AddTransient<IProductData, InMemoryProductData>();
            services.AddDbContext<WebStoreContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                //.EnableSensitiveDataLogging(true) // for debugging 
                //.LogTo(Console.WriteLine)    
                );

            services.AddTransient<WebStoreDbInitializer>();

            //services AddMvc();

            services
                .AddControllersWithViews(
                mvc =>
                {
                    //mvc.Conventions.Add(new ActionDescriptionAttribute("base desc"));
                    mvc.Conventions.Add(new ApplicationConvention());
                })
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.Map(); owen PO adding.
            //app.Use(); delegate or class in conveyor

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
