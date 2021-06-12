namespace WebApplication.Models.WinRate
{
    public class WrByRole
    {
        public Struct.WinRate Damage { get; set; }
        public Struct.WinRate Support { get; set; }
        public Struct.WinRate Tank { get; set; }
        public Struct.WinRate OpenQueue { get; set; }
    }
}