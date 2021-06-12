using System.Linq;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Helpers
{
    public class SeasonHelper
    {
        public static Season GetLastSeason(DbSet<Season> dbSet)
        {
            return dbSet.OrderByDescending(s => s.Number).First();
        }
    }
}