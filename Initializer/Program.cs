using System.IO;
using Microsoft.Extensions.Configuration;

namespace Initializer;

internal class Program
{
    private static IConfiguration _configuration;

    private static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false);

        _configuration = builder.Build();
    }
}