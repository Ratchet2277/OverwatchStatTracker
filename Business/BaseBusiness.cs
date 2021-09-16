using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public abstract class BaseBusiness
    {
        protected readonly Task<User> CurrentUser;

        protected BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            CurrentUser = userManager.GetUserAsync(user);
        }

        protected UserManager<User> UserManager { get; set; }
        private IServiceProvider _serviceProvider;
    }
}