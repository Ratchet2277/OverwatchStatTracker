using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public abstract class BaseController : Controller
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