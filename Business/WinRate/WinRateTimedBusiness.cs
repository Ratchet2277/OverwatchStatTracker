#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using ViewModel.Contract;

namespace Business.WinRate;

public partial class WinRateBusiness
{
    public async Task<IChartJsOptions?> ByWeekDays()
    {
        var data = new ChartJsData<double>
        {
            Labels = new List<string>(),
            DataSets = new List<DataSet<double>>
            {
                new("% Win") { BackgroundColor = new List<string> { WinColor } },
                new("% Draw") { BackgroundColor = new List<string> { DrawColor } }
            }
        };

        var winRatePerWeekDay = (await repository.Find(await UserManager.GetUserAsync(UClaimsPrincipal), true)
                .BySeason(await seasonBusiness.GetLastSeason())
                .ToListAsync())
            .GroupBy(g => g.DateTime.DayOfWeek)
            .OrderBy(g => g.Key)
            .ToDictionary(group => group.Key, GetWinRate);


        foreach (var (dayOfWeek, winRate) in winRatePerWeekDay)
        {
            data.Labels.Add(dayOfWeek.ToString());
            data.DataSets[0].AddData(winRate.Rate * 100);
            data.DataSets[1].AddData(winRate.DrawRate * 100);
        }

        return new ChartJsOptions<double>
        {
            Type = "bar",
            Data = data,
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

    public async Task<IChartJsOptions?> ByHours(DayOfWeek? dayOfWeek = null)
    {
        var data = new ChartJsData<double>
        {
            Labels = new List<string>(),
            DataSets = new List<DataSet<double>>
            {
                new("# Win") { BackgroundColor = new List<string> { WinColor } },
                new("# Draw") { BackgroundColor = new List<string> { DrawColor } },
                new("# Lose") { BackgroundColor = new List<string> { LoseColor } }
            }
        };

        var gameRepository = repository.Find(await UserManager.GetUserAsync(UClaimsPrincipal), true)
            .BySeason(await seasonBusiness.GetLastSeason());

        if (dayOfWeek is not null) gameRepository = gameRepository.ByDayOfWeek((DayOfWeek)dayOfWeek);

        var gameQuery = gameRepository.Query;

        if (gameQuery is null)
            return null;


        for (var i = 0; i <= 23; i++)
        {
            var i1 = i;
            var hourQuery = gameQuery.Where(g => g.DateTime.Hour == i1);
            var totalCount = hourQuery.Count();
            if (totalCount > 0)
            {
                data.DataSets[0]
                    .AddData(hourQuery.Count(g => g.AllieScore > g.EnemyScore));
                data.DataSets[1]
                    .AddData(hourQuery.Count(g => g.AllieScore == g.EnemyScore));
                data.DataSets[2]
                    .AddData(hourQuery.Count(g => g.AllieScore < g.EnemyScore));
            }
            else
            {
                data.DataSets[0].AddData(0);
                data.DataSets[1].AddData(0);
                data.DataSets[2].AddData(0);
            }

            data.Labels.Add($"{i1}H");
        }

        return new ChartJsOptions<double>
        {
            Type = "bar",
            Data = data,
            Options = new
            {
                maintainAspectRatio = false,
                responsive = true,
                scales = new
                {
                    x = new
                    {
                        stacked = true
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