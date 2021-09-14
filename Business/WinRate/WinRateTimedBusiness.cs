#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.EntityFrameworkCore;
using ViewModel.Contract;

namespace Business.WinRate
{
    public partial class WinRateBusiness
    {
        public async Task<IChartJsOptions?> ByWeekDays()
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

            var winRatePerWeekDay = _gameRepository.Find(currentUser, true)
                .BySeason(season)
                .Query
                ?.GroupBy(g => g.DateTime.DayOfWeek)
                .OrderBy(g => g.Key)
                .ToDictionary(group => group.Key, GetWinRate);

            if (winRatePerWeekDay is null)
                return null;

            foreach (var (dayOfWeek, winRate) in winRatePerWeekDay)
            {
                data.Labels.Add(dayOfWeek.ToString());
                data.DataSets[0].AddData(winRate.Rate);
                data.DataSets[1].AddData(winRate.DrawRate);
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

    }
}