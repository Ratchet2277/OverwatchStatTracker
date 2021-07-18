using System.Diagnostics;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(TrackerContext context, ILogger<HomeController> logger, UserManager<User> userManager) : base(context, logger, userManager)
        {
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}