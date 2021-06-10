using System;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackerContextExtension context = new TrackerContextExtension();

            context.Initialize(true);
        }
    }
}