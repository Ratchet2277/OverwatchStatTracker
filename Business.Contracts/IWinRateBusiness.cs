#nullable enable
using System;
using System.Threading.Tasks;
using DomainModel.Types;
using ViewModel.Contract;

namespace Business.Contracts
{
    public interface IWinRateBusiness
    {
        public Task<IChartJsOptions?> ByHero(Role? role = null);
        public Task<IChartJsOptions?> ByType();
        public Task<IChartJsOptions?> ByWeekDays();
        public Task<IChartJsOptions?> ByHours(DayOfWeek? dayOfWeek = null);
    }
}