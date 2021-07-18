using System.Collections.Generic;
using DomainModel;
using WebApplication.Buisness;

namespace WebApplication.Models
{
    public class GameListComponentModel
    {
        public List<Game> Games { get; set; }

        public SrEvolution SrEvolution { get; set; }

    }
}