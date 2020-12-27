using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SportStore.Microservice.Notification.Domain.Interfaces;
using SportStore.Microservice.Notification.Domain.Services;
using SportStore.Microservice.Notification.Domain.Settings.SendGrid;

namespace SportStore.Microservice.Notification.Infra.CrossCutting
{
    public static class Injections
    {
        public static void AddRegisterServicesNotifications(this IServiceCollection services)
        {
            services.AddSingleton<ISendGridSettings>(sp =>
            sp.GetRequiredService<IOptions<SendGridSettings>>().Value);

            services.AddTransient<IEmailService, EmailService>();
        }
    }
}