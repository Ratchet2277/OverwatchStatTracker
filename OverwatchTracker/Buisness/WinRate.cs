#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using WebApplication.Models;

namespace WebApplication.Buisness
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class WinRate : BaseBuisness
    {
        private readonly SeasonBuisness _seasonBuisness;

        public WinRate(TrackerContext context, UserManager<User> userManager, SeasonBuisness seasonBuisness,
            ClaimsPrincipal user) : base(context, userManager, user)
        {
            _seasonBuisness = seasonBuisness;
        }

        public async Task<ChartJsOptions<double>?> ByHero()
        {
            var season = _seasonBuisness.GetLastSeason();

            var currentUser = await UserManager.GetUserAsync(User);

            var datas = new ChartJsData<double>
            {
                Labels = new List<string>(),
                DataSets = new List<DataSet<double>>
                {
                    new("% Win"),
                    new("% Draw")
                }
            };

            var heroList = season.HeroPool
                .Where(h => h.Games.Any(g => g.User == currentUser))
                .OrderByDescending(h =>
                    (double) h.Games.Where(g => g.User == currentUser).Count(g => g.AllieScore >= g.EnemyScore) /
                    h.Games.Count);

            if (!heroList.Any())
                return null;

            datas.Labels.AddRange(heroList.Select(h => h.Name).ToList());

            datas.DataSets[0].AddBacgroundColor("#03a9f4")
                .AddData(heroList.Select(h =>
                    (double) h.Games.Where(g => g.User == currentUser).Count(g => g.AllieScore > g.EnemyScore) /
                    h.Games.Count * 100).ToList());

            datas.DataSets[1].AddBacgroundColor("#ffeb3b")
                .AddData(heroList.Select(h =>
                    (double) h.Games.Where(g => g.User == currentUser).Count(g => g.AllieScore == g.EnemyScore) /
                    h.Games.Count * 100).ToList());

            return new ChartJsOptions<double>
            {
                Data = datas,
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

        public async Task<ChartJsOptions<double>?> ByType()
        {
            var season = _seasonBuisness.GetLastSeason();
            var labels = new List<string>();
            var currentUser = await UserManager.GetUserAsync(User);


            List<DataSet<double>> dataSets = new()
            {
                new DataSet<double>("% Win").AddBacgroundColor("#03a9f4"),
                new DataSet<double>("% Draw").AddBacgroundColor("#ffeb3b"),
                new DataSet<double>("% Lose").AddBacgroundColor("#f44336")
            };

            var winRates = WinRateHelper.WrByRole(season.Games.Where(g => g.User == currentUser));

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