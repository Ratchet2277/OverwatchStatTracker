﻿#nullable enable
using System.Threading.Tasks;
using DomainModel.Types;
using ViewModel.Contract;

namespace Business.Contracts
{
    public interface IWinRate
    {
        public Task<IChartJsOptions?> ByHero(Role? role = null);
        public Task<IChartJsOptions?> ByType();
    }
}