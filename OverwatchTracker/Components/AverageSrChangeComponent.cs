using System;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Buisness;

namespace WebApplication.Components
{
    public class AverageSrChangeComponent : BaseComponent
    {
        public AverageSrChangeComponent(TrackerContext context, IServiceProvider serviceProvider) : base(context,
            serviceProvider)
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await ServiceProvider.GetService<SrEvolution>()?.GetAverageEvolution()!);
        }
    }
}