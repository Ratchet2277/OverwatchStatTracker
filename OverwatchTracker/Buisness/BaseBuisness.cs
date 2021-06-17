using DAL;

namespace WebApplication.Buisness
{
    public class BaseBuisness
    {
        public BaseBuisness(TrackerContext context)
        {
            Context = context;
        }

        protected TrackerContext Context { get; set; }
    }
}