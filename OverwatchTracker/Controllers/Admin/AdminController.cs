using System;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tracker.Controllers.Admin;

[Route("Admin/")]
[Authorize(Roles = "Admin")]
public class AdminController : BaseController
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(ILogger<AdminController> logger, UserManager<User> userManager,
        IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager) : base(logger, userManager,
        serviceProvider)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        return View();
    }
}