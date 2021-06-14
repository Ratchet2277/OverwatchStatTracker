using System.Collections.Generic;
using DAL;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Buisness;
using WebApplication.Models.Contracts;

namespace WebApplication.Controllers
{
    public class ChartJsController : BaseController
    {
        public ChartJsController(TrackerContext context, ILogger<ChartJsController> logger) : base(context, logger)
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