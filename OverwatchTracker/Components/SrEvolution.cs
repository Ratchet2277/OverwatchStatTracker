using System.Collections.Generic;
using System.Linq;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Components
{
    public class SrEvolution : BaseComponent
    {
        public SrEvolution(TrackerContext context) : base(context)
        {
            
        }

        public IViewComponentResult Invoke()
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);

            var group = season.Games.GroupBy(g => g.Type);
            
            List<ChartJsData<int>> datas = new List<ChartJsData<int>>();

            foreach (IGrouping<GameType,Game> games in group)
            {
                var query = games.OrderBy(g => g.DateTime).TakeLast(10);
                datas.Add(new ChartJsData<int>
                {
                    // ReSharper disable once PossibleMultipleEnumeration
                    Labels = query.Select(g => g.DateTime.ToString()).ToList(),
                    DataSets = new List<DataSet<int>>
                    {
                        new ($"{games.Key.ToString()} Rank")
                        {
                            // ReSharper disable once PossibleMultipleEnumeration
                            Data = query.Select(g => g.Sr).ToList(),
                            BorderColor = "#f44336",
                            BackgroundColor = new List<string> {"#f44336"},
                            Stepped = true
                        }
                    }
                });
            }
            
            
            return View(datas);
        }
    }
}