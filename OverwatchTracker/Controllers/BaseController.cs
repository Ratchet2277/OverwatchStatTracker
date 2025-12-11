using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers;

public abstract class BaseController(ILogger<BaseController> logger, UserManager<User> userManager)
    : Controller
{
    protected readonly ILogger<BaseController> Logger = logger;
    protected readonly UserManager<User> UserManager = userManager;
}