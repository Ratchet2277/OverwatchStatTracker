using DAL;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Buisness
{
    public class BaseBuisness
    {
        protected TrackerContext Context { get; set; }

        public BaseBuisness(TrackerContext context)
        {
            Context = context;
        }
    }
}