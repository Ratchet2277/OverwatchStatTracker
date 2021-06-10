using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public class BaseController : Controller
    {
        
        protected readonly TrackerContext Context;
        protected readonly ILogger<BaseController> Logger;

        public BaseController(TrackerContext context, ILogger<BaseController> logger)
        {
            Context = context;
            Logger = logger;
        }

        
    }
}