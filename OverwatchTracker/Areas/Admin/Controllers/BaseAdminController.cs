using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers;

namespace WebApplication.Areas.Admin.Controllers;

public abstract class BaseAdminController : BaseController
{
    protected BaseAdminController(ILogger<BaseAdminController> logger, UserManager<User> userManager) : base(logger,
        userManager)
    {
    }
}