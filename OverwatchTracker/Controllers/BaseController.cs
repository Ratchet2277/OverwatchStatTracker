using System;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly TrackerContext Context;
        protected readonly ILogger<BaseController> Logger;
        protected readonly UserManager<User> UserManager;
        
        protected readonly IServiceProvider ServiceProvider;

        public BaseController(TrackerContext context, ILogger<BaseController> logger, UserManager<User> userManager, IServiceProvider serviceProvider)
        {
            Context = context;
            Logger = logger;
            UserManager = userManager;
            ServiceProvider = serviceProvider;
        }
    }
}