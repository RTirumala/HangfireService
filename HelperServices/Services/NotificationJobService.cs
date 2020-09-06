using Microsoft.Extensions.Logging;
using System;

namespace HelperServices
{
    public class NotificationJobService : INotificationJobService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public NotificationJobService(IEmailService emailService, ILogger<NotificationJobService> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }
        public void NotificationEmails(string user, string time)
        {
            Console.WriteLine("Notify Users....");
            _logger.LogInformation("Sending Notification Emails...");
            _emailService.sendEmail();
        }
    }

    public interface INotificationJobService
    {
        public void NotificationEmails(string user, string time);
    }
}
