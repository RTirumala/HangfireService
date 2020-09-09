using Autofac;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using HelperServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Extensions.Autofac.DependencyInjection;
using Serilog;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;

namespace HanfireWorker
{
    public class HangfireWorker : BackgroundService
    {
        private readonly INotificationJobService _notificationJobService;
        private readonly ILogger<HangfireWorker> _logger;
        private readonly IConfiguration _configuration;
        //private BackgroundJobServer _server;
        public HangfireWorker(IConfiguration configuration, INotificationJobService notificationJobService, ILogger<HangfireWorker> logger)
        {
            _configuration = configuration;
            _notificationJobService = notificationJobService;
            _logger = logger;

            var builder = new ContainerBuilder();

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
            serviceCollection.BuildServiceProvider();
            builder.Populate(serviceCollection);
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(config);
            builder.RegisterSerilog(loggerConfig); //(@"C:\Logs\autofac");
            //builder.RegisterType<IHttpClientFactory>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<HangfireJobService>();
            builder.RegisterType<NotificationJobService>();
            
            var container = builder.Build();
            //var factory = container.Resolve<IHttpClientFactory>();

            GlobalConfiguration.Configuration.UseAutofacActivator(container, false);
            GlobalConfiguration.Configuration.UseSqlServerStorage(_configuration.GetConnectionString("Hangfire"));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_logger.LogInformation("Hangfire Job started...");
            //try
            //{
            //    _server = new BackgroundJobServer();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Hangfire Job threw and exception:", ex);
            //    throw;
            //}
            //_logger.LogInformation("Hangfire Job started...");
            using (new BackgroundJobServer())
            {
                Console.WriteLine("Background server started...");
                Console.ReadLine();
            }
            return Task.CompletedTask;

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hangfire Job stopping...");
            //_server.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
