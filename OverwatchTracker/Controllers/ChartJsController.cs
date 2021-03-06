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
public class ChartJsController : BaseController
{
    private readonly IServiceProvider _serviceProvider;

    public ChartJsController(ILogger<ChartJsController> logger,
        UserManager<User> userManager, IServiceProvider serviceProvider) : base(logger, userManager)
    {
        _serviceProvider = serviceProvider;
    }

    // GET
    [HttpGet("ChartJs/Get/{id}")]
    public async Task<IChartJsOptions?> Get(string id)
    {
        return id switch
        {
            "wr-by-hero" => await _serviceProvider.GetService<IWinRateBusiness>()?.ByHero()!,
            "wr-by-type" => await _serviceProvider.GetService<IWinRateBusiness>()?.ByType()!,
            "wr-by-day-of-week" => await _serviceProvider.GetService<IWinRateBusiness>()?.ByWeekDays()!,
            "wr-by-hours" => await _serviceProvider.GetService<IWinRateBusiness>()?.ByHours()!,
            "sr-evolution-damage" => await _serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.Damage)!,
            "sr-evolution-support" => await _serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.Support)!,
            "sr-evolution-tank" => await _serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.Tank)!,
            "sr-evolution-open-queue" => await _serviceProvider.GetService<ISrEvolutionBusiness>()
                ?.ByType(GameType.OpenQueue)!,
            _ => throw new KeyNotFoundException()
        };
    }
}