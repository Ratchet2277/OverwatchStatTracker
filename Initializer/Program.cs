using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Initializer
{
    internal class Program
    {
        private static void Main()
        {
            // var context = new TrackerContextExtension();

            // context.Initialize(true);
        }

        public class TrackerContextFactory : IDesignTimeDbContextFactory<TrackerContext>
        {
            public TrackerContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TrackerContext>();
                optionsBuilder.UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrackerDB;Integrated Security=true",
                    b => b.MigrationsAssembly("Initializer"));

                return new TrackerContext(optionsBuilder.Options);
            }
        }
    }
}