using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TrackerContext : DbContext
    {
        public TrackerContext(DbContextOptions options) : base(options)
        {
        }

        public TrackerContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SquadMember> SquadMembers { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}