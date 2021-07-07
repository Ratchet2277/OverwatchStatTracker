using DAL;
using DomainModel;

namespace WebApplication.Buisness
{
    public class BaseBuisness
    {
        public BaseBuisness(TrackerContext context, User currentUser)
        {
            Context = context;
            CurrentUser = currentUser;
        }

        protected User CurrentUser { get; set; }
        protected TrackerContext Context { get; set; }
    }
}