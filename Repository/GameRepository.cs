#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using DomainModel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Repository.Contracts;

namespace Repository
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(TrackerContext context) : base(context)
        {
        }

        public IQueryable<Game>? Query { get; private set; }

        public Game[] ToArray()
        {
            if (Query is null) throw new IncompleteInitialization();
            return Query.ToArray();
        }

        public async Task<Game[]> ToArrayAsync()
        {
            if (Query is null) throw new IncompleteInitialization();
            return await Query.ToArrayAsync();
        }

        public List<Game> ToList()
        {
            if (Query is null) throw new IncompleteInitialization();
            return Query.ToList();
        }

        public async Task<List<Game>> ToListAsync()
        {
            if (Query is null) throw new IncompleteInitialization();
            return await Query.ToListAsync();
        }

        public async Task<Game> Get(int id)
        {
            return await Context.Games.FindAsync(id);
        }

        public async Task Add(Game game)
        {
            if (game.User is null)
                throw new ArgumentException("Game.User must be set before being added to the repository");

            game.DateTime = DateTime.Now;
            game.Map = await Context.Maps.FindAsync(game.NewMap);
            game.Heroes =
                new Collection<Hero>(await Context.Heroes.Where(h => game.NewHeroes.Contains(h.Id)).ToListAsync());

            if (game.NewSquadMembers.Length > 0)
            {
                game.SquadMembers = new Collection<SquadMember>();
                foreach (var squadMember in game.NewSquadMembers)
                {
                    if (Context.SquadMembers.Any(s => s.Name == squadMember && s.MainUser == game.User))
                    {
                        game.SquadMembers.Add(Context.SquadMembers.First(s =>
                            s.Name == squadMember && s.MainUser == game.User));
                        continue;
                    }

                    game.SquadMembers.Add(new SquadMember { Name = squadMember, MainUser = game.User });
                }
            }

            Context.Games.Add(game);

            await Context.SaveChangesAsync();
        }

        public async Task Update(Game newGame)
        {
            var game = await Get(newGame.Id);

            game.AllieScore = newGame.AllieScore;
            game.EnemyScore = newGame.EnemyScore;
            game.Type = newGame.Type;
            game.Sr = newGame.Sr;

            if (game.Map.Id != newGame.NewMap) game.Map = await Context.Maps.FindAsync(newGame.NewMap);

            foreach (var heroToDel in game.Heroes.Where(h => !newGame.NewHeroes.Contains(h.Id)).ToList())
                game.Heroes.Remove(heroToDel);

            foreach (var heroToAdd in Context.Heroes.Where(h =>
                newGame.NewHeroes.Contains(h.Id) && !game.Heroes.Contains(h))) game.Heroes.Add(heroToAdd);

            foreach (var squadMemberName in newGame.NewSquadMembers)
            {
                if (game.SquadMembers.Any(s => newGame.NewSquadMembers.Contains(s.Name)))
                    continue;

                if (Context.SquadMembers.Any(s => s.Name == squadMemberName && s.MainUser == game.User))
                {
                    game.SquadMembers.Add(Context.SquadMembers.First(s => s.Name == squadMemberName));
                    continue;
                }

                game.SquadMembers.Add(new SquadMember { Name = squadMemberName, MainUser = game.User });
            }

            foreach (var squadMemberToDel in game.SquadMembers.Where(s => !newGame.NewSquadMembers.Contains(s.Name))
                .ToList())
                game.SquadMembers.Remove(squadMemberToDel);

            Context.Games.Update(game);

            await Context.SaveChangesAsync();
        }


        public void Find(User currentUser)
        {
            if (Query is null) throw new IncompleteInitialization();
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
            Query = invert
                ? Query.Where(g => g.AllieScore ! < g.EnemyScore)
                : Query.Where(g => g.AllieScore > g.EnemyScore);
        }

        public void Lose(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert
                ? Query.Where(g => g.AllieScore ! > g.EnemyScore)
                : Query.Where(g => g.AllieScore < g.EnemyScore);
        }

        public void Draw(bool invert = false)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = invert
                ? Query.Where(g => g.AllieScore != g.EnemyScore)
                : Query.Where(g => g.AllieScore == g.EnemyScore);
        }
    }
}