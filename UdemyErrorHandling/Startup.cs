using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyErrorHandling.Filter;

namespace UdemyErrorHandling
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
            services.AddMvc(options =>
            {

                options.Filters.Add(new CustomHandleExceptionFilterAttribute() { ErrorPage = "error2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // midllewears 
            //  Request ------------[DeveloperExceptionPage]---------[ExceptionHandler]------------[UseStatusCodePages]------------[UseDatabaseErrorPage]------------------- >Response


            if (env.IsDevelopment())
            {
        
                app.UseDeveloperExceptionPage();
                // 1-ci-yol
                app.UseStatusCodePages("text/plain","Bir xeta var.Xeta kodu:{0}");
                //2-ci yol
                app.UseStatusCodePages(async context =>
                {
                    context.HttpContext.Response.ContentType = "text/plain";
                    await context.HttpContext.Response.WriteAsync($"Bir xeta var.Xeta kodu:{context.HttpContext.Response.StatusCode}");

                });
                // 3-cu yol
                //app.UseStatusCodePages();

                //app.UseDatabaseErrorPage();
            }
            else
            {
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseExceptionHandler(context=> {

            //    context.Run(async page =>
            //    {

            //        page.Response.StatusCode = 500;
            //        page.Response.ContentType = "text/html";
            //        await page.Response.WriteAsync($"<html><head></head><h1>Xeta var:{page.Response.StatusCode}</h1></html>");
            //    });
                
            //});
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
