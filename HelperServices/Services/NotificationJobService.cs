using System;

namespace HelperServices
{
    public class NotificationJobService : INotificationJobService
    {
        private readonly IEmailService _emailService;
        //private readonly ILogger<NotificationJobService> _logger;

        public NotificationJobService(IEmailService emailService)
        {
            _emailService = emailService;
            //_logger = logger;
        }
        public void NotificationEmails(string user, string time)
        {
            Console.WriteLine("Notify Users....");
            //_logger.LogInformation("Sending Notification Emails...");
            _emailService.sendEmail();
        }
    }

    public interface INotificationJobService
    {
        public void NotificationEmails(string user, string time);
    }
}
