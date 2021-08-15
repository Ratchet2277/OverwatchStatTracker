using Business;
using Business.Contracts;
using DAL;
using DataModel;
using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Contracts;
using ViewModel.Contract;

namespace WebApplication
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
            services.AddScoped<UserManager<User>>();

            #region Business

            services.AddScoped<ISrEvolution, SrEvolution>();
            services.AddScoped<IWinRate, WinRate>();
            services.AddScoped<ISeasonBusiness, SeasonBusiness>();
            services.AddScoped<IGameBusiness, GamesBusiness>();
            services.AddScoped<ISquadMemberBusiness, SquadMemberBusiness>();

            #endregion

            #region Repository

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ISquadMemberRepository, SquadMemberRepository>();

            #endregion

            #region ViewModel

            services.AddScoped<IPagination<Game>, Pagination<Game>>();

            #endregion

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(p => p.GetRequiredService<IHttpContextAccessor>().HttpContext?.User);

            services.AddDbContext<TrackerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TrackerDB"))
            );

            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Identity",
                    "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}