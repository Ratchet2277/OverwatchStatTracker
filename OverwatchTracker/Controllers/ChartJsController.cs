#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Contracts;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModel.Contract;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ChartJsController : BaseController
    {
        public ChartJsController(ILogger<ChartJsController> logger,
            UserManager<User> userManager, IServiceProvider serviceProvider) : base(logger, userManager,
            serviceProvider)
        {
        }

        // GET
        [HttpGet("ChartJs/Get/{id}")]
        public async Task<IChartJsOptions?> Get(string id)
        {
            return id switch
            {
                "wr-by-hero" => await ServiceProvider.GetService<IWinRateBusiness>()?.ByHero()!,
                "wr-by-type" => await ServiceProvider.GetService<IWinRateBusiness>()?.ByType()!,
                "wr-by-day-of-week" => await ServiceProvider.GetService<IWinRateBusiness>()?.ByWeekDays()!,
                "wr-by-hours" => await ServiceProvider.GetService<IWinRateBusiness>()?.ByHours()!,
                "sr-evolution-damage" => await ServiceProvider.GetService<ISrEvolutionBusiness>()?.ByType(GameType.Damage)!,
                "sr-evolution-support" => await ServiceProvider.GetService<ISrEvolutionBusiness>()?.ByType(GameType.Support)!,
                "sr-evolution-tank" => await ServiceProvider.GetService<ISrEvolutionBusiness>()?.ByType(GameType.Tank)!,
                "sr-evolution-open-queue" => await ServiceProvider.GetService<ISrEvolutionBusiness>()
                    ?.ByType(GameType.OpenQueue)!,
                _ => throw new KeyNotFoundException()
            };
        }
    }
}