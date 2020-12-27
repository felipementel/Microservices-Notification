namespace SportStore.Microservice.Notification.Domain.Settings.SendGrid
{
    public class SendGridSettings : ISendGridSettings
    {
        public string SendGridAPIKey { get; set; }

        public string EmailFrom { get; set; }

        public string SenderName { get; set; }
    }

    public interface ISendGridSettings
    {
        string SendGridAPIKey { get; set; }

        string EmailFrom { get; set; }

        string SenderName { get; set; }
    }
}