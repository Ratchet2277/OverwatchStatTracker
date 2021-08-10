#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Types;
using ViewModel.Contract;

namespace Business.Contracts
{
    public interface ISrEvolution
    {
        public Task<IChartJsOptions?> ByType(GameType? type);
        public Task<Dictionary<GameType, Tuple<float, float>>> GetAverageEvolution();
        public Task<int?> DeltaLastGame(Game game);
    }
}