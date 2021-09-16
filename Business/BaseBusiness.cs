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

        protected BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user)
        {
            CurrentUser = userManager.GetUserAsync(user);
        }

    }
}