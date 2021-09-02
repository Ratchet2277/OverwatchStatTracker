using System;
using System.Security.Claims;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public abstract class BaseBusiness
    {
        protected readonly ClaimsPrincipal User;

        protected BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            UserManager = userManager;
            User = user;
            ServiceProvider = serviceProvider;
        }

        protected UserManager<User> UserManager { get; set; }
        protected IServiceProvider ServiceProvider { get; }
    }
}