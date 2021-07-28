#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication.Business;
using WebApplication.Models.Contracts;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ChartJsController : BaseController
    {

        public ChartJsController(TrackerContext context, ILogger<ChartJsController> logger,
            UserManager<User> userManager, IServiceProvider serviceProvider) : base(context, logger, userManager, serviceProvider)
        {
        }

        // GET
        [HttpGet("ChartJs/Get/{id}")]
        public async Task<IChartJsOptions?> Get(string id)
        {
            return id switch
            {
                "wr-by-hero" => await ServiceProvider.GetService<WinRate>()?.ByHero()!,
                "wr-by-type" => await ServiceProvider.GetService<WinRate>()?.ByType()!,
                "sr-evolution-damage" => await ServiceProvider.GetService<SrEvolution>()?.ByType(GameType.Damage)!,
                "sr-evolution-support" => await ServiceProvider.GetService<SrEvolution>()?.ByType(GameType.Support)!,
                "sr-evolution-tank" => await ServiceProvider.GetService<SrEvolution>()?.ByType(GameType.Tank)!,
                "sr-evolution-open-queue" => await ServiceProvider.GetService<SrEvolution>()
                    ?.ByType(GameType.OpenQueue)!,
                _ => throw new KeyNotFoundException()
            };
        }
    }
}