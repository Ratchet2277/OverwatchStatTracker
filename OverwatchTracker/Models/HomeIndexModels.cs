using System;
using System.Collections.Generic;
using DomainModel;
using DomainModel.Types;
using WebApplication.Buisness;

namespace WebApplication.Models
{
    public class HomeIndexModels
    {
        public List<Game> Games { get; set; }
        
        public Dictionary<GameType, Tuple<float, float>> SrDeltaAverage { get; set; }
        
        public SrEvolution SrEvolution { get; set; }
    }
}