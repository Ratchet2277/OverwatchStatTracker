using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("Admin/Dashboard")]
public class DashboardController : BaseAdminController
{
    public DashboardController(ILogger<DashboardController> logger, UserManager<User> userManager) : base(logger,
        userManager)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}