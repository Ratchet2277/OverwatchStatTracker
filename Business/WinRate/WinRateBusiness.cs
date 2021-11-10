#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Contracts;
using DataModel;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;
using ViewModel.Contract;

namespace Business.WinRate;

public partial class WinRateBusiness : BaseBusiness, IWinRateBusiness
{
    private const string WinColor = "#03a9f4";
    private const string DrawColor = "#ffeb3b";
    private const string LoseColor = "#f44336";
    private readonly IGameRepository _gameRepository;
    private readonly ISeasonBusiness _seasonBusiness;

    public WinRateBusiness(UserManager<User> userManager, ISeasonBusiness seasonBusiness,
        ClaimsPrincipal user, IGameRepository gameRepository) : base(userManager,
        user)
    {
        _gameRepository = gameRepository;
        _seasonBusiness = seasonBusiness;
    }

    public async Task<IChartJsOptions?> ByHero(Role? role = null)
    {
        var season = await _seasonBusiness.GetLastSeason();
        var user = await UserManager.GetUserAsync(UClaimsPrincipal);

        var data = new ChartJsData<double>
        {
            Labels = new List<string>(),
            DataSets = new List<DataSet<double>>
            {
                new("% Win") { BackgroundColor = new List<string> { WinColor } },
                new("% Draw") { BackgroundColor = new List<string> { DrawColor } }
            }
        };

        bool GameFilter(Game g)
        {
            return g.User == user && g.Season == season;
        }

        //listing of heroes to show, with ordering
        var heroList = season.HeroPool
            .Where(h => h.Games.Any(GameFilter)
                        && (role == null || h.Role == role)
            )
            .OrderByDescending(h =>
            {
                var winRate = GetWinRate(h.Games.Where(GameFilter));
                return winRate.Rate + winRate.DrawRate;
            });

        if (!heroList.Any())
            return null;

        data.Labels.AddRange(heroList.Select(h => h.Name).ToList());

        foreach (var winRate in heroList.Select(hero => GetWinRate(hero.Games.Where(GameFilter))))
        {
            data.DataSets[0].AddData(winRate.Rate * 100);
            data.DataSets[1].AddData(winRate.DrawRate * 100);
        }

        return new ChartJsOptions<double>
        {
            Data = data,
            Type = "bar",
            Options = new
            {
                responsive = true,
                indexAxis = 'y',
                scales = new
                {
                    x = new
                    {
                        min = 0,
                        max = 100,
                        stacked = true
                    },
                    y = new
                    {
                        stacked = true
                    }
                },
                maintainAspectRatio = false,
                elements = new
                {
                    bar = new
                    {
                        borderWidth = 0
                    }
                }
            }
        };
    }

    public async Task<IChartJsOptions?> ByType()
    {
        var labels = new List<string>();

        List<DataSet<double>> dataSets = new()
        {
            new DataSet<double>("% Win").AddBackgroundsColor(WinColor),
            new DataSet<double>("% Draw").AddBackgroundsColor(DrawColor),
            new DataSet<double>("% Lose").AddBackgroundsColor(LoseColor)
        };

        var gameQuery = _gameRepository.Find(await UserManager.GetUserAsync(UClaimsPrincipal), true)
            .BySeason(await _seasonBusiness.GetLastSeason());

        var winRates = WrByRole(await gameQuery.ToListAsync());

        if (!winRates.Any())
            return null;

        foreach (var (type, winRate) in winRates)
        {
            dataSets[0].AddData(winRate.Rate * 100);
            dataSets[1].AddData(winRate.DrawRate * 100);
            dataSets[2].AddData((1 - winRate.Rate - winRate.DrawRate) * 100);

            labels.Add(type == GameType.OpenQueue ? "Open Queue" : type.ToString());
        }

        ChartJsData<double> data = new()
        {
            Labels = labels,
            DataSets = dataSets
        };

        return new ChartJsOptions<double>
        {
            Data = data,
            Type = "bar",
            Options = new
            {
                responsive = true,
                scales = new
                {
                    x = new
                    {
                        stacked = true
                    },
                    y = new
                    {
                        min = 0,
                        max = 100,
                        stacked = true
                    }
                }
            }
        };
    }
}