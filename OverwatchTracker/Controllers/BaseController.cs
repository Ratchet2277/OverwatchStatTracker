using System;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tracker.Controllers;

public abstract class BaseController : Controller
{
    protected readonly ILogger<BaseController> Logger;

    protected readonly IServiceProvider ServiceProvider;
    protected readonly UserManager<User> UserManager;

    protected BaseController(ILogger<BaseController> logger, UserManager<User> userManager,
        IServiceProvider serviceProvider)
    {
        Logger = logger;
        UserManager = userManager;
        ServiceProvider = serviceProvider;
    }
}