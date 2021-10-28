using DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TrackerContext : IdentityDbContext<User>
    {
        //*
        public TrackerContext(DbContextOptions options) : base(options)
        {
        }
        //*/

        public TrackerContext()
        {
        }

        // public override DbSet<User> Users { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SquadMember> SquadMembers { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // only for dotnet-ef tool
            /*
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=TrackerDB;Integrated Security=true",
                b => b.MigrationsAssembly("Initializer"));
            //*/
            /*
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=TrackerDB;Trusted_Connection=True;",
                b => b.MigrationsAssembly("Initializer"));
            //*/

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}