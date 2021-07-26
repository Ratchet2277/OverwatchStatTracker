using System.Collections.Generic;
using DomainModel;
using WebApplication.Business;

namespace WebApplication.Models
{
    public class GameListModel
    {
        public List<Game> Games { get; set; }

        public SrEvolution SrEvolution { get; set; }
    }
}