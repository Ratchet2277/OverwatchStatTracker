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
        private readonly IServiceProvider _serviceProvider;

        public ChartJsController(TrackerContext context, ILogger<ChartJsController> logger,
            UserManager<User> userManager, IServiceProvider serviceProvider) : base(context, logger, userManager)
        {
            _serviceProvider = serviceProvider;
        }

        // GET
        [HttpGet("ChartJs/Get/{id}")]
        public async Task<IChartJsOptions?> Get(string id)
        {
            return id switch
            {
                "wr-by-hero" => await _serviceProvider.GetService<WinRate>()?.ByHero()!,
                "wr-by-type" => await _serviceProvider.GetService<WinRate>()?.ByType()!,
                "sr-evolution-damage" => await _serviceProvider.GetService<SrEvolution>()?.ByType(GameType.Damage)!,
                "sr-evolution-support" => await _serviceProvider.GetService<SrEvolution>()?.ByType(GameType.Support)!,
                "sr-evolution-tank" => await _serviceProvider.GetService<SrEvolution>()?.ByType(GameType.Tank)!,
                "sr-evolution-open-queue" => await _serviceProvider.GetService<SrEvolution>()
                    ?.ByType(GameType.OpenQueue)!,
                _ => throw new KeyNotFoundException()
            };
        }
    }
}