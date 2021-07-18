using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Buisness;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(TrackerContext context, ILogger<HomeController> logger, UserManager<User> userManager) : base(context, logger, userManager)
        {
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);
            var srEvolution = new SrEvolution(Context, user);
            var model = new HomeIndexModels
            {
                Games = Context.Games.Where(g => g.User == user).OrderByDescending(g => g.DateTime).Take(10).ToList(),
                SrDeltaAverage = srEvolution.GetAverageEvolution(),
                SrEvolution = srEvolution
            };
            return View(model);
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