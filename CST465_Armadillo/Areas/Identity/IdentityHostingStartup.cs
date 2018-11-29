using System;
using CST465_Armadillo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CST465_Armadillo.Areas.Identity.IdentityHostingStartup))]
namespace CST465_Armadillo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CST465_ArmadilloContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CST465_ArmadilloContextConnection")));

                //This is the "Old way", but is required to make the 
                //[Authorize(Roles="Admins")] functionality to work
                services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<CST465_ArmadilloContext>();

                //This is the default way that .NET Core recommends
                //services.AddDefaultIdentity<IdentityUser>(options =>
                //{
                //    options.Password.RequireDigit = false;
                //    options.Password.RequireLowercase = false;
                //    options.Password.RequireNonAlphanumeric = false;
                //    options.Password.RequireUppercase = true;
                //    options.Password.RequiredLength = 8;
                //    options.Password.RequiredUniqueChars = 1;
                //})
                //    .AddRoles<IdentityRole>()
                //    .AddEntityFrameworkStores<CST465_ArmadilloContext>();
            });
        }
    }
}