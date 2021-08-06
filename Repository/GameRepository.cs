#nullable enable
using System.Collections.Generic;
using System.Linq;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.VisualBasic.CompilerServices;
using Repository.Contracts;

namespace Repository
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private IQueryable<Game>? Query { get; set; }
        
        public GameRepository(TrackerContext context) : base(context)
        {
        }

        public void ToArray()
        {
            throw new System.NotImplementedException();
        }

        public void ToArrayAsync()
        {
            throw new System.NotImplementedException();
        }

        public void ToList()
        {
            throw new System.NotImplementedException();
        }

        public void ToListAsync()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> GetAllGames(User user, GameType? type = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> GetAllWin(User user, GameType? type = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> GetAllLose(User user, GameType? type = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> GetAllDraw(User user, GameType? type = null)
        {
            throw new System.NotImplementedException();
        }

        public void Find(User currentUser)
        {
            Query = Context.Games.Where(g => g.User == currentUser);
        }

        public void ByHero(Hero hero)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.Heroes.Contains(hero));
        }

        public void ByType(GameType gameType)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.Type == gameType);
        }

        public void BySeason(Season season)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(g => g.Season == season);
        }

        public void Win(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert ? Query.Where(g => g.AllieScore !< g.EnemyScore) : Query.Where(g => g.AllieScore > g.EnemyScore);
        }

        public void Lose(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert ? Query.Where(g => g.AllieScore !> g.EnemyScore) : Query.Where(g => g.AllieScore < g.EnemyScore);
        }

        public void Draw(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert ? Query.Where(g => g.AllieScore != g.EnemyScore) : Query.Where(g => g.AllieScore == g.EnemyScore);
        }
    }
}