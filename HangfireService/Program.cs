using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HanfireWorker;
using HelperServices.IoC;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;
using Autofac;

namespace HangfireService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSerilog()
                
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.RegisterHelperServices();
                    services.AddHostedService<HangfireWorker>();
                });
    }
}
