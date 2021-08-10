#nullable enable
using System.Threading.Tasks;
using ViewModel.Contract;

namespace Business.Contracts
{
    public interface IWinRate
    {
        public Task<IChartJsOptions?> ByHero();
        public Task<IChartJsOptions?> ByType();
    }
}