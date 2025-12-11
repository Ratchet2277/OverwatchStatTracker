using System.Security.Claims;
using DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Business;

public abstract class BaseBusiness(UserManager<User> userManager, ClaimsPrincipal user)
{
    protected readonly ClaimsPrincipal UClaimsPrincipal = user;

    protected readonly UserManager<User> UserManager = userManager;
}