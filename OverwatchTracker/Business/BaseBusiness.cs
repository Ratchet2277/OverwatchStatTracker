using System.Security.Claims;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Business
{
    public class BaseBusiness
    {
        protected ClaimsPrincipal User;

        public BaseBusiness(TrackerContext context, UserManager<User> userManager, ClaimsPrincipal user)
        {
            Context = context;
            UserManager = userManager;
            User = user;
        }

        protected UserManager<User> UserManager { get; set; }
        protected TrackerContext Context { get; set; }
    }
}