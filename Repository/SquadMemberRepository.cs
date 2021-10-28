using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.VisualBasic.CompilerServices;
using Repository.Contracts;

namespace Repository
{
    public class SquadMemberRepository : BaseRepository<SquadMember>, ISquadMemberRepository
    {
        public SquadMemberRepository(TrackerContext context) : base(context)
        {
        }

        public async Task<SquadMember> Get(int id)
        {
            return await Context.SquadMembers.FindAsync(id);
        }

        public async Task Add(SquadMember squadMember)
        {
            Context.SquadMembers.Add(squadMember);
            await Context.SaveChangesAsync();
        }

        public async Task Update(SquadMember squadMember)
        {
            Context.SquadMembers.Update(squadMember);
            await Context.SaveChangesAsync();
        }

        public ISquadMemberRepository Find(User user)
        {
            Query = Context.SquadMembers.Where(s => s.MainUser == user);
            return this;
        }

        public bool Any()
        {
            if (Query is null) throw new IncompleteInitialization();
            return Query.Any();
        }

        public ISquadMemberRepository ByName(string name)
        {
            if (Query is null) throw new IncompleteInitialization();
            Query = Query.Where(s => s.Name == name);
            return this;
        }
    }
}