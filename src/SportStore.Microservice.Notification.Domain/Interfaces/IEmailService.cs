using SendGrid;
using SportStore.Microservice.Notification.Domain.Model;
using System.Threading.Tasks;

namespace SportStore.Microservice.Notification.Domain.Interfaces
{
    public interface IEmailService
    {
        Task<Response> EnviarEmail(EmailCompose emailCompose);
    }
}
