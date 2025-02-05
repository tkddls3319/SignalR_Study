using Microsoft.AspNetCore.SignalR;

namespace SignalRStudy
{
    public class MessageHub : Hub
    {
        public async Task Send(string message, string userid)
        {
            await Clients.All.SendAsync("receive", message, userid);//응답을 기다리지 않음 리턴값 없음, 순 알림, 브로드캐스트, 명령 실행
        }

    }
}
