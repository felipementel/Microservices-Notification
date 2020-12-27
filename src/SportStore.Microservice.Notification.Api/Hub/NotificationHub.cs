using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SportStore.Microservice.Notification.Domain
{
    public class NotificationHub : Hub
    {
        public async Task BroadcastMessage(string name, string message)
        {
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public void Echo(string name, string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync("echo", name, message + " (echo from server)");
        }
    }
}
