using System;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelperServices;

namespace HangfireDashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly INotificationJobService _notificationJobService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackgroundJobClient backgroundJobClient, INotificationJobService notificationJobService)
        {
            _logger = logger;
            _backgroundJobClient = backgroundJobClient;
            _notificationJobService = notificationJobService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            var rng = new Random();
            _backgroundJobClient.Enqueue<NotificationJobService>((x) => x.NotificationEmails("123","21:30"));
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public void ThisIsHangfireMethod()
        {
            Console.WriteLine("Hello world from Hangfire!");
        }
    }
}
