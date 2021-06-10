using System;
using DAL;

namespace Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackerContext context = new TrackerContext();

            context.Database.EnsureCreated();
        }
    }
}