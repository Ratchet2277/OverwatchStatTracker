using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

// ReSharper disable All

namespace WebApplication.Components.WinRate
{
    public class WrByHero : BaseComponent
    {
        public WrByHero(TrackerContext context) : base(context)
        {
        }

        public IViewComponentResult Invoke()
        {
            var season = SeasonHelper.GetLastSeason(Context.Seasons);

            var datas = new ChartJsData<double>()
            {
                Labels = new(),
                DataSets = new()
                {
                    new DataSet<double>("% Win"),
                    new DataSet<double>("% Draw")
                }
            };

            var heroList = season.HeroPool
                .Where(h => h.Games.Count > 0)
                .OrderByDescending(h => (double) h.Games.Count(g => g.AllieScore >= g.EnemyScore) / h.Games.Count);

            datas.Labels.AddRange(heroList.Select(h => h.Name).ToList());

            datas.DataSets[0].AddBacgroundColor("#03a9f4")
                .AddData(heroList.Select(h =>
                    (double) h.Games.Count(g => g.AllieScore > g.EnemyScore) / h.Games.Count * 100).ToList());

            datas.DataSets[1].AddBacgroundColor("#ffeb3b")
                .AddData(heroList.Select(h =>
                    (double) h.Games.Count(g => g.AllieScore == g.EnemyScore) / h.Games.Count * 100).ToList());

            return View(datas);
        }
    }
}