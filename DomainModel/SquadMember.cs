using DomainModel.Types;

namespace DomainModel
{
    public class SquadMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}