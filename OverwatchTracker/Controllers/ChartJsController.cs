#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IChartJsOptions?> Get(string id)
        {
            var user = await UserManager.GetUserAsync(User);
            return id switch
            {
                "wr-by-hero" => new WinRate(Context, user).ByHero(),
                "wr-by-type" => new WinRate(Context, user).ByType(),
                "sr-evolution-damage" => new SrEvolution(Context, user).ByType(GameType.Damage),
                "sr-evolution-support" => new SrEvolution(Context, user).ByType(GameType.Support),
                "sr-evolution-tank" => new SrEvolution(Context, user).ByType(GameType.Tank),
                "sr-evolution-open-queue" => new SrEvolution(Context, user).ByType(GameType.OpenQueue),
                _ => throw new KeyNotFoundException()
            };
        }
    }
}