using System;
using System.Threading.Tasks;
using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker.Components;

public class AverageSrChangeComponent : BaseComponent
{
    public AverageSrChangeComponent(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await ServiceProvider.GetService<ISrEvolutionBusiness>()?.GetAverageEvolution()!);
    }
}