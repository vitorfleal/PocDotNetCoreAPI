using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.IO;

namespace AE.HealthSystem.Services.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                       .UseContentRoot(Directory.GetCurrentDirectory())
                       .UseSerilog((hostingContext, loggerConfiguration) =>
                       {
                           loggerConfiguration.MinimumLevel
                               .Debug()
                               .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                               .Enrich.FromLogContext()
                               .WriteTo.RollingFile(Path.Combine(@"C:\PocDotNetCoreAPI\LogFiles\Application\HealthSystem.Api\", "log-{Date}.log"));
                       })
                       .UseStartup<Startup>();
                });
    }
}
