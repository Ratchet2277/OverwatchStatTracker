using System.Security.Claims;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public abstract class BaseBusiness
    {
        protected readonly ClaimsPrincipal UClaimsPrincipal;

        protected readonly UserManager<User> UserManager;

        protected BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user)
        {
            UserManager = userManager;
            UClaimsPrincipal = user;
        }
    }
}