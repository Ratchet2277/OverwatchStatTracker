using System.Security.Claims;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public abstract class BaseBusiness
    {

        protected readonly UserManager<User> UserManager;
        protected readonly ClaimsPrincipal uClaimsPrincipal;
        protected BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user)
        {
            UserManager = userManager;
            uClaimsPrincipal = user;
        }
    }
}