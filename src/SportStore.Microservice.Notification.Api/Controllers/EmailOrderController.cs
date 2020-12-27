using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Microservice.Notification.Domain.Interfaces;
using SportStore.Microservice.Notification.Domain.Model;
using SportStore.Microservice.Notification.Domain.Settings.SendGrid;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SportStore.Microservice.Notification.Api.Controllers
{
    [Authorize(Roles = "Client")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class EmailOrderController : ControllerBase
    {
        private readonly ISendGridSettings _sendGridSettings;
        public EmailOrderController(ISendGridSettings sendGridSettings)
        {
            _sendGridSettings = sendGridSettings;
        }

        [HttpPost("NewOrder/{userId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> NewOrder([FromServices] IEmailService enviarEmail, EmailCompose emailCompose)
        {
            if (ModelState.IsValid)
            {
                emailCompose.UserId = User.FindFirst("sub")?.Value;
                await enviarEmail.EnviarEmail(emailCompose);
            }

            return BadRequest();
        }
    }
}