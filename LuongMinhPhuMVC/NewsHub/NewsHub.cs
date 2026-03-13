using Microsoft.AspNetCore.SignalR;

namespace LuongMinhPhuMVC.NewsHub
{
    public class NewsHub : Hub
    {
        public async Task BroadcastNews()
        {
            await Clients.All.SendAsync("ReloadNews");
        }
    }
}
