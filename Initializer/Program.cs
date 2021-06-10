using System;
using DAL;

namespace Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackerContext context = new TrackerContext(null);

            context.Initialize(true);
        }
    }
}