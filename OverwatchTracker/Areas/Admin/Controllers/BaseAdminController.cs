using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tracker.Controllers;

namespace Tracker.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public abstract class BaseAdminController : BaseController
{
    protected BaseAdminController(ILogger<BaseAdminController> logger, UserManager<User> userManager) : base(logger,
        userManager)
    {
    }
}