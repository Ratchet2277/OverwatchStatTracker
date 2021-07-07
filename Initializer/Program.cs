using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Initializer
{
    internal class Program
    {
        private static void Main()
        {
            var context = new TrackerContextExtension();

            context.Initialize(true);
        }
    }
}