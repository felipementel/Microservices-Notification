using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SportStore.Microservice.Notification.Api.Controllers
{
    //[Authorize(Roles = "Salesman")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;

        private readonly IHubContext<Domain.NotificationHub> _hubContext;
        int i = 0;

        public NotificationController(
            ILogger<NotificationController> logger,
            IHubContext<Domain.NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpPost("NewOrder/{ConnectionId}")]
        public async Task<IActionResult> Post(string ConnectionId)
        {
            Teste t = new Teste
            {
                Id = i + 1,
                Descricao = "descricao"
            };

            await _hubContext.Clients.Client(ConnectionId).SendAsync("newOrder", JsonConvert.SerializeObject(t));
            await _hubContext.Clients.All.SendAsync("newOrder", JsonConvert.SerializeObject(t));

            return Ok();
        }
    }


    public class Teste
    {
        public int Id { get; set; } = 0;

        public string Descricao { get; set; }
    }
}