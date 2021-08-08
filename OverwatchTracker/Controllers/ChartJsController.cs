#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business;
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
                "wr-by-hero" => await ServiceProvider.GetService<WinRate>()?.ByHero()!,
                "wr-by-type" => await ServiceProvider.GetService<WinRate>()?.ByType()!,
                "sr-evolution-damage" => await ServiceProvider.GetService<ISrEvolution>()?.ByType(GameType.Damage)!,
                "sr-evolution-support" => await ServiceProvider.GetService<ISrEvolution>()?.ByType(GameType.Support)!,
                "sr-evolution-tank" => await ServiceProvider.GetService<ISrEvolution>()?.ByType(GameType.Tank)!,
                "sr-evolution-open-queue" => await ServiceProvider.GetService<ISrEvolution>()
                    ?.ByType(GameType.OpenQueue)!,
                _ => throw new KeyNotFoundException()
            };
        }
    }
}