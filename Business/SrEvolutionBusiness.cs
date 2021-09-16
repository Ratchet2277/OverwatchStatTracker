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
    public class SrEvolutionBusiness : BaseBusiness, ISrEvolutionBusiness

    {
        private readonly IGameBusiness _business;
        private readonly Task<Season> _currentSeason;
        private readonly IGameRepository _repository;

        public SrEvolutionBusiness(UserManager<User> userManager, ISeasonBusiness seasonBusiness,
            ClaimsPrincipal user, IGameBusiness business, IGameRepository repository)
            : base(userManager, user)
        {
            _repository = repository;
            _business = business;
            _currentSeason = seasonBusiness.GetLastSeason();
        }

        public async Task<IChartJsOptions?> ByType(GameType? type)
        {
            var games = _repository.Find(await CurrentUser).BySeason(await _currentSeason);

            if (type is not null) games.ByType((GameType)type);

            var list = await games.OrderByDate().ToListAsync();

            if (list is null || !list.Any()) return null;

            var data = new ChartJsData<int>
            {
                Labels = list.Select(g => g.DateTime.ToString()).ToList(),


                DataSets = new List<DataSet<int>>
                {
                    new($"{(type == GameType.OpenQueue ? "Open Queue" : type.ToString())} Rank")
                    {
                        Data = list.Select(g => g.Sr).ToList(),
                        BorderColor = "#f44336",
                        BackgroundColor = new List<string> { "#f44336" },
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

        public async Task<Dictionary<GameType, Tuple<float, float>>> GetAverageEvolution()
        {
            var result = new Dictionary<GameType, Tuple<float, float>>();

            foreach (var gameType in (GameType[])Enum.GetValues(typeof(GameType)))
            {
                var evolutionByType = await GetAverageEvolutionByType(gameType);
                if (evolutionByType is null)
                    continue;
                result.Add(gameType, evolutionByType);
            }

            return result;
        }

        public async Task<int?> DeltaLastGame(Game game)
        {
            var previousGame = await _business.GetPreviousGame(game);

            if (previousGame != null)
                return game.Sr - previousGame.Sr;

            return null;
        }

        private async Task<Tuple<float, float>?> GetAverageEvolutionByType(GameType? type)
        {
            //get all games of this season, no need to count draw since they always keep the same SR 
            var gameRepository = _repository.Find(await CurrentUser).BySeason(await _currentSeason).Draw(true);

            if (type != null)
                gameRepository.ByType((GameType)type);

            if (!gameRepository.Any())
                return null;

            var listGames = await gameRepository.OrderByDate().ToListAsync();

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

            return new Tuple<float, float>((float)totalWin / nbWin, (float)totalLose / nbLose);
        }
    }
}