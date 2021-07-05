using System.Collections.Generic;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Buisness;
using WebApplication.Models.Contracts;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ChartJsController : BaseController
    {
        public ChartJsController(TrackerContext context, ILogger<ChartJsController> logger, UserManager<User> userManager) : base(context, logger, userManager)
        {
        }
        
        // GET
        [HttpGet("ChartJs/Get/{id}")]
        public IChartJsOptions Get(string id)
        {
            return id switch
            {
                "wr-by-hero" => new WinRate(Context).ByHero(),
                "wr-by-type" => new WinRate(Context).ByType(),
                "sr-evolution-damage" => new SrEvolution(Context).ByType(GameType.Damage),
                "sr-evolution-support" => new SrEvolution(Context).ByType(GameType.Support),
                "sr-evolution-tank" => new SrEvolution(Context).ByType(GameType.Tank),
                "sr-evolution-open-queue" => new SrEvolution(Context).ByType(GameType.OpenQueue),
                _ => throw new KeyNotFoundException()
            };
        }
    }
}