using System.Collections.Generic;
using System.Data;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;
using WebApplication.Models.WinRate;

namespace WebApplication.Components
{
    public class WrByRoleComponent : BaseComponent
    {
        public WrByRoleComponent(TrackerContext context) : base(context)
        {
        }
        
        public IViewComponentResult Invoke()
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);
            
            List<DataSet<double>> dataSets = new()
            {
                new DataSet<double>("% Win").AddBacgroundColor("#03a9f4"),
                new DataSet<double>("% Draw").AddBacgroundColor("#ffeb3b"),
                new DataSet<double>("% Lose").AddBacgroundColor("#f44336")
            };

            var winRates = WinRateHelper.WrByRole(season.Games);

            foreach (var winRate in winRates)
            {
                dataSets[0].AddData(winRate.Value.Rate * 100);
                dataSets[1].AddData(winRate.Value.DrawRate * 100);
                dataSets[2].AddData((1 - winRate.Value.Rate - winRate.Value.DrawRate) * 100);
            }

            ChartJsData<double> data = new()
            {
                Labels = new List<string> {"Damage", "Support", "Tank", "Open Queue"},
                DataSets = dataSets
            };

            return View(data);
        }
    }
}