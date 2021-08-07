using System.Security.Claims;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class BaseBusiness
    {
        protected ClaimsPrincipal User;

        public BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user)
        {
            UserManager = userManager;
            User = user;
        }

        protected UserManager<User> UserManager { get; set; }
    }
}