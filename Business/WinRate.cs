#nullable enable
using System;
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

namespace Business
{
    public partial class WinRate : BaseBusiness, IWinRate
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISeasonBusiness _seasonBusiness;

        public WinRate(UserManager<User> userManager, ISeasonBusiness seasonBusiness,
            ClaimsPrincipal user, IServiceProvider serviceProvider, IGameRepository gameRepository) : base(userManager,
            user, serviceProvider)
        {
            _seasonBusiness = seasonBusiness;
            _gameRepository = gameRepository;
        }

        public async Task<IChartJsOptions?> ByHero(Role? role = null)
        {
            var season = await _seasonBusiness.GetLastSeason();

            var currentUser = await UserManager.GetUserAsync(User);

            var data = new ChartJsData<double>
            {
                Labels = new List<string>(),
                DataSets = new List<DataSet<double>>
                {
                    new("% Win"),
                    new("% Draw")
                }
            };

            var heroList = season.HeroPool
                .Where(h => h.Games.Any(g => g.User == currentUser && g.Season == season) &&
                            (role == null || h.Role == role))
                .OrderByDescending(h =>
                    (double)h.Games.Count(g =>
                        g.AllieScore >= g.EnemyScore && g.User == currentUser && g.Season == season) /
                    h.Games.Count(g => g.User == currentUser && g.Season == season)
                );

            if (!heroList.Any())
                return null;

            data.Labels.AddRange(heroList.Select(h => h.Name).ToList());

            data.DataSets[0].AddBacgroundColor("#03a9f4")
                .AddData(heroList.Select(h =>
                        (double)h.Games.Count(g =>
                            g.AllieScore > g.EnemyScore && g.User == currentUser && g.Season == season) /
                        h.Games.Count(g => g.User == currentUser && g.Season == season) * 100).ToList()
                );

            data.DataSets[1].AddBacgroundColor("#ffeb3b")
                .AddData(heroList.Select(h =>
                        (double)h.Games.Count(g =>
                            g.AllieScore == g.EnemyScore && g.User == currentUser && g.Season == season) /
                        h.Games.Count(g => g.User == currentUser && g.Season == season) * 100).ToList()
                );

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
            var season = await _seasonBusiness.GetLastSeason();
            var labels = new List<string>();
            var currentUser = await UserManager.GetUserAsync(User);


            List<DataSet<double>> dataSets = new()
            {
                new DataSet<double>("% Win").AddBacgroundColor("#03a9f4"),
                new DataSet<double>("% Draw").AddBacgroundColor("#ffeb3b"),
                new DataSet<double>("% Lose").AddBacgroundColor("#f44336")
            };

            var gameQuery = _gameRepository.Find(currentUser, true)
                .BySeason(season);

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
}