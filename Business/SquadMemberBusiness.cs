using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using Business.Contracts;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;

namespace Business
{
    public class SquadMemberBusiness : BaseBusiness, ISquadMemberBusiness
    {
        private readonly ISquadMemberRepository _repository;

        public SquadMemberBusiness(UserManager<User> userManager, ClaimsPrincipal user,
            IServiceProvider serviceProvider, ISquadMemberRepository repository) : base(userManager, user,
            serviceProvider)
        {
            _repository = repository;
        }

        public Game EditSquadMemberList(ref Game game, string[] names)
        {
            game.SquadMembers ??= new Collection<SquadMember>();

            foreach (var name in names)
            {
                if (game.SquadMembers.Any(s => names.Contains(s.Name)))
                    continue;

                var query = _repository.Find(game.User).ByName(name);

                if (query.Any())
                {
                    game.SquadMembers.Add(query.First());
                    continue;
                }

                game.SquadMembers.Add(new SquadMember { Name = name, MainUser = game.User });
            }

            foreach (var squadMemberToDel in game.SquadMembers.Where(s => !names.Contains(s.Name))
                .ToList())
                game.SquadMembers.Remove(squadMemberToDel);

            return game;
        }
    }
}