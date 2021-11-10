using DomainModel;

namespace Business.Contracts;

public interface ISquadMemberBusiness
{
    public Game EditSquadMemberList(ref Game game, string[] names);
}