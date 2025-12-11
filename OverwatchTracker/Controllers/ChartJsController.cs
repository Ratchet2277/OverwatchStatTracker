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

namespace WebApplication.Controllers;

[Authorize]
[Route("[controller]")]
public class ChartJsController(
    ILogger<ChartJsController> logger,
    UserManager<User> userManager,
    IServiceProvider serviceProvider)
    : BaseController(logger, userManager)
{
    // GET
    [HttpGet("Get/{id}")]
    public async Task<IChartJsOptions?> Get(string id)
    {
        return id switch
        {
            "wr-by-hero" => await serviceProvider.GetService<IWinRateBusiness>()?.ByHero()!,
            "wr-by-type" => await serviceProvider.GetService<IWinRateBusiness>()?.ByType()!,
            "wr-by-day-of-week" => await serviceProvider.GetService<IWinRateBusiness>()?.ByWeekDays()!,
            "wr-by-hours" => await serviceProvider.GetService<IWinRateBusiness>()?.ByHours()!,
            "sr-evolution-damage" => await serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.Damage)!,
            "sr-evolution-support" => await serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.Support)!,
            "sr-evolution-tank" => await serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.Tank)!,
            "sr-evolution-open-queue" => await serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.OpenQueue)!,
            _ => throw new KeyNotFoundException()
        };
    }
}