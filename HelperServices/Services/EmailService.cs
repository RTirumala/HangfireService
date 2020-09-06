using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperServices
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public void sendEmail()
        {
            Console.WriteLine("Email is sent...");
            _logger.LogInformation("Email is sent...");
        }
    }

    public interface IEmailService
    {
        public void sendEmail();
    }
}
