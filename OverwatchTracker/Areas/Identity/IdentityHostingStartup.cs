using DAL;
using DomainModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace WebApplication.Areas.Identity;

public class IdentityHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            services.AddDbContext<TrackerContext>(options =>
                options.UseSqlServer(
                    context.Configuration.GetConnectionString("TrackerDB")));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TrackerContext>();
        });
    }
}