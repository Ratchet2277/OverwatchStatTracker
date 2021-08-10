using System;
using System.Security.Claims;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public abstract class BaseBusiness
    {
        protected ClaimsPrincipal User;

        public BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            UserManager = userManager;
            User = user;
            ServiceProvider = serviceProvider;
        }

        protected UserManager<User> UserManager { get; set; }
        protected IServiceProvider ServiceProvider { get; }
    }
}