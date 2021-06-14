using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DAL;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Buisness
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class WinRate : BaseBuisness
    {
        public WinRate(TrackerContext context) : base(context)
        {
        }

        public ChartJsOptions<double> ByHero()
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);

            var datas = new ChartJsData<double>()
            {
                Labels = new(),
                DataSets = new()
                {
                    new DataSet<double>("% Win"),
                    new DataSet<double>("% Draw")
                }
            };

            var heroList = season.HeroPool
                .Where(h => h.Games.Count > 0)
                .OrderByDescending(h => (double) h.Games.Count(g => g.AllieScore >= g.EnemyScore) / h.Games.Count);

            datas.Labels.AddRange(heroList.Select(h => h.Name).ToList());

            datas.DataSets[0].AddBacgroundColor("#03a9f4")
                .AddData(heroList.Select(h =>
                    (double) h.Games.Count(g => g.AllieScore > g.EnemyScore) / h.Games.Count * 100).ToList());

            datas.DataSets[1].AddBacgroundColor("#ffeb3b")
                .AddData(heroList.Select(h =>
                    (double) h.Games.Count(g => g.AllieScore == g.EnemyScore) / h.Games.Count * 100).ToList());

            return new ChartJsOptions<double>()
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
                            stacked = true,
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

        public ChartJsOptions<double> ByType()
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);

            List<DataSet<double>> dataSets = new()
            {
                new DataSet<double>("% Win").AddBacgroundColor("#03a9f4"),
                new DataSet<double>("% Draw").AddBacgroundColor("#ffeb3b"),
                new DataSet<double>("% Lose").AddBacgroundColor("#f44336")
            };

            var winRates = WinRateHelper.WrByRole(season.Games);

            foreach (var (_, winRate) in winRates)
            {
                dataSets[0].AddData(winRate.Rate * 100);
                dataSets[1].AddData(winRate.DrawRate * 100);
                dataSets[2].AddData((1 - winRate.Rate - winRate.DrawRate) * 100);
            }

            ChartJsData<double> data = new()
            {
                Labels = new List<string> {"Damage", "Support", "Tank", "Open Queue"},
                DataSets = dataSets
            };

            return new ChartJsOptions<double>()
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
                            stacked = true,
                        },
                        y = new
                        {
                            stacked = true
                        }
                    }
                }
            };
        }
    }
}