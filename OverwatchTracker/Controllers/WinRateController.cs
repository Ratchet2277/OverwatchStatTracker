using System;
using System.Linq;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Helpers;
using WebApplication.Models.WinRate;

namespace WebApplication.Controllers
{
    public class WinRateController : BaseController
    {
        public WinRateController(TrackerContext context, ILogger<WinRateController> logger) : base(context, logger)
        {
            
        }
        
        
    }
}