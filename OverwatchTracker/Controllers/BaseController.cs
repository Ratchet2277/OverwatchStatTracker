using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> Logger;
        protected readonly UserManager<User> UserManager;

        protected BaseController(ILogger<BaseController> logger, UserManager<User> userManager)
        {
            Logger = logger;
            UserManager = userManager;
        }
    }
}