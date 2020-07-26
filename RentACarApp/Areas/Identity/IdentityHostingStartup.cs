using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACarApp.Areas.Identity.Data;
using RentACarApp.Data;

[assembly: HostingStartup(typeof(RentACarApp.Areas.Identity.IdentityHostingStartup))]
namespace RentACarApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RentACarAppDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RentACarAppDbContextConnection")));

                services.AddDefaultIdentity<RentACarAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<RentACarAppDbContext>();
            });
        }
    }
}