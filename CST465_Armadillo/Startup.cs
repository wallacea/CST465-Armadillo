﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ArmadilloLib;
using Microsoft.Extensions.Configuration;
using CST465_Armadillo.Repositories;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Identity;
using CST465_Armadillo.Models;
using Microsoft.EntityFrameworkCore;

namespace CST465_Armadillo
{
    public class Startup
    {
        public IConfiguration _Configuration;
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc();
            services.Configure<FarmSettings>(_Configuration);
            services.Configure<ArmadilloSettings>(_Configuration);
            services.AddTransient<IArmadilloRepository, InjectableArmadilloDBRepository>();
            
                
            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = _Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = _Configuration["Authentication:Facebook:AppSecret"];
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(options => options.MapRoute("Default", "{controller=Armadillo}/{action=Index}/{id?}"));
            string baseDir = env.ContentRootPath;

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(baseDir, "App_Data"));

        }
    }
}
