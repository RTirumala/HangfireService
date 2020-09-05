using System;
using System.Collections.Generic;
using System.Text;

namespace HelperServices
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }
        public void sendEmail()
        {
            Console.WriteLine("Email is sent...");
        }
    }

    public interface IEmailService
    {
        public void sendEmail();
    }
}
