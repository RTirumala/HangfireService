using Autofac;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using HelperServices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HanfireWorker
{
    public class HangfireWorker : BackgroundService
    {
        private readonly INotificationJobService _notificationJobService;
        private readonly IConfiguration _configuration;
        private BackgroundJobServer _server;
        public HangfireWorker(IConfiguration configuration, INotificationJobService notificationJobService)
        {
            _configuration = configuration;
            _notificationJobService = notificationJobService;
            var builder = new ContainerBuilder();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<NotificationJobService>();
            GlobalConfiguration.Configuration.UseAutofacActivator(builder.Build(), false);
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
                _notificationJobService.NotificationEmails("test", "test2");
                Console.ReadLine();
            }
            return Task.CompletedTask;

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Hangfire Job stopping...");
            //_server.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
