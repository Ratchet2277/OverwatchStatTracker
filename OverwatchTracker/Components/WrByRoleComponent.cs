using System.Collections.Generic;
using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

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

            foreach (var (_, winRate) in winRates)
            {
                dataSets[0].AddData(winRate.Rate * 100);
                dataSets[1].AddData(winRate.DrawRate * 100);
                dataSets[2].AddData((1 - winRate.Rate - winRate.DrawRate) * 100);
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