using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperServices.IoC
{
    public static class HelperModuleExtension
    {
        public static void RegisterHelperServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailService, EmailService>();
            serviceCollection.AddTransient<INotificationJobService, NotificationJobService>();
        }
    }
}
