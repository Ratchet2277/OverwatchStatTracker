using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class TrackerContext : IdentityDbContext<User, IdentityRole, string>
{
    //*
    public TrackerContext(DbContextOptions options) : base(options)
    {
    }
    //*/

    public TrackerContext()
    {
    }
    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Map> Maps { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<SquadMember> SquadMembers { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
}