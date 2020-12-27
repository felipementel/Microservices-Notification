using SendGrid;
using SendGrid.Helpers.Mail;
using SportStore.Microservice.Notification.Domain.Interfaces;
using SportStore.Microservice.Notification.Domain.Model;
using SportStore.Microservice.Notification.Domain.Settings.SendGrid;
using System.Threading.Tasks;

namespace SportStore.Microservice.Notification.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridSettings _sendGridSettings;

        public EmailService(ISendGridSettings sendGridSettings)
        {
            _sendGridSettings = sendGridSettings;
        }

        public async Task<Response> EnviarEmail(EmailCompose emailCompose)
        {
            var apiKey = _sendGridSettings.SendGridAPIKey;

            var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("admin@felipementel.dev.br", "Felipe Augusto");
            var from = new EmailAddress(_sendGridSettings.EmailFrom, _sendGridSettings.SenderName);
            var subject = "Sport Store - Products you should buy";
            var to = new EmailAddress(emailCompose.EmailDestination, emailCompose.EmailDestinationName);
            var plainTextContent = "TEXTO";
            var htmlContent = emailCompose.EmailBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }
    }
}
