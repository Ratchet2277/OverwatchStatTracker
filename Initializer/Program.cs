using System.IO;
using Microsoft.Extensions.Configuration;

namespace Initializer;

internal static class Program
{
    private static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false);

        builder.Build();
    }
}