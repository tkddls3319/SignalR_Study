using Microsoft.AspNetCore.SignalR;

namespace SignalRStudy
{
    public class MessageHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("receive", message);
        }
    }
}
