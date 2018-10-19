using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ArmadilloLib;

namespace CST465_Armadillo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes => {
            //    routes.MapRoute("default", "{controller=Armadillo}/{action=Index}/{id?}");
            //    routes.MapRoute("otherroute", "peanut-butter/{action=Index}/{id?}", new { controller = "Home" });
            //});
            
        }
    }
}
