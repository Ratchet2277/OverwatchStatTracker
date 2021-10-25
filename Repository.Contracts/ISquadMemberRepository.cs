#nullable enable
using System.Linq;
using DomainModel;

namespace Repository.Contracts;

public interface ISquadMemberRepository : IBaseRepository<SquadMember>
{
    public IQueryable<SquadMember>? Query { get; }
    public ISquadMemberRepository Find(User user);
    public bool Any();
    public ISquadMemberRepository ByName(string name);
}