#nullable enable
using System.Collections.Generic;
using System.Linq;
using DAL;
using DomainModel;
using DomainModel.Types;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Buisness
{
    public class SrEvolution : BaseBuisness

    {
        public SrEvolution(TrackerContext context, User currentUser) : base(context, currentUser)
        {
        }

        public ChartJsOptions<int>? ByType(GameType? type)
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);

            var games = season.Games.Where(g => g.User == CurrentUser);

            if (type is not null)
            {
                games = games.Where(g => g.Type == type);
            }
            
            var query = games.OrderBy(g => g.DateTime).TakeLast(10);

            if (!query.Any())
            {
                return null;
            }

            var data = new ChartJsData<int>
            {
                // ReSharper disable once PossibleMultipleEnumeration
                Labels = query.Select(g => g.DateTime.ToString()).ToList(),


                DataSets = new List<DataSet<int>>
                {
                    new($"{(type == GameType.OpenQueue ? "Open Queue" : type.ToString())} Rank")
                    {
                        // ReSharper disable once PossibleMultipleEnumeration
                        Data = query.Select(g => g.Sr).ToList(),
                        BorderColor = "#f44336",
                        BackgroundColor = new List<string> {"#f44336"},
                        Stepped = true
                    }
                }
            };
            return new ChartJsOptions<int>()
            {
                Data = data,
                Type = "line",
                Options = new
                {
                    responsive = true,
                    elements = new
                    {
                        point = new
                        {
                            radius = 0
                        }
                    },
                    interaction = new
                    {
                        intersect = false,
                        mode = "index",
                    },
                    plugins = new
                    {
                        tooltip = new
                        {
                            position = "nearest",
                        }
                    }
                }
            };
        }
    }
}