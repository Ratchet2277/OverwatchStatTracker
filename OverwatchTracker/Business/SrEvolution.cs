#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Identity;
using WebApplication.Models;

namespace WebApplication.Business
{
    public class SrEvolution : BaseBusiness

    {
        private readonly SeasonBusiness _seasonBusiness;

        public SrEvolution(TrackerContext context, UserManager<User> userManager, SeasonBusiness seasonBusiness,
            ClaimsPrincipal user) : base(context, userManager, user)
        {
            _seasonBusiness = seasonBusiness;
        }

        public async Task<ChartJsOptions<int>?> ByType(GameType? type)
        {
            var season = _seasonBusiness.GetLastSeason();

            var currentUser = await UserManager.GetUserAsync(User);

            var games = season.Games.Where(g => g.User == currentUser);

            if (type is not null) games = games.Where(g => g.Type == type);

            var query = games.OrderBy(g => g.DateTime);

            if (!query.Any()) return null;

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
            return new ChartJsOptions<int>
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
                        mode = "index"
                    },
                    plugins = new
                    {
                        tooltip = new
                        {
                            position = "nearest"
                        }
                    }
                }
            };
        }

        private async Task<Tuple<float, float>?> GetAverageEvolutionByType(GameType? type)
        {
            var season = _seasonBusiness.GetLastSeason();

            var currentUser = await UserManager.GetUserAsync(User);

            //no need to count draw since they always keep the same SR 
            var games = season.Games.Where(g => g.User == currentUser && g.EnemyScore != g.AllieScore);

            if (type is not null)
                games = games.Where(g => g.Type == type);

            if (!games.Any())
                return null;

            var listGames = games.OrderBy(g => g.DateTime).ToList();

            var totalWin = 0;
            var nbWin = 0;
            var totalLose = 0;
            var nbLose = 0;

            for (var i = 1; i < listGames.Count; i++)
            {
                if (listGames[i].AllieScore > listGames[i].EnemyScore)
                {
                    totalWin += listGames[i].Sr - listGames[i - 1].Sr;
                    nbWin++;
                    continue;
                }

                totalLose += listGames[i].Sr - listGames[i - 1].Sr;
                nbLose++;
            }

            return new Tuple<float, float>((float) totalWin / nbWin, (float) totalLose / nbLose);
        }

        public async Task<Dictionary<GameType, Tuple<float, float>>> GetAverageEvolution()
        {
            var result = new Dictionary<GameType, Tuple<float, float>>();

            foreach (var gameType in (GameType[]) Enum.GetValues(typeof(GameType)))
            {
                var evolutionByType = await GetAverageEvolutionByType(gameType);
                if (evolutionByType is null)
                    continue;
                result.Add(gameType, evolutionByType);
            }

            return result;
        }

        public int? DeltaLastGame(Game game)
        {
            var previousGameQuery = Context.Games
                .Where(g => g.DateTime < game.DateTime && g.User == game.User && g.Type == game.Type)
                .OrderByDescending(g => g.DateTime);

            if (previousGameQuery.Any())
                return game.Sr - previousGameQuery.First().Sr;

            return null;
        }
    }
}