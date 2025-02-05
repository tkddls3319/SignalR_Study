using Microsoft.AspNetCore.SignalR;

namespace SignalRStudy
{
    public class MessageHub : Hub
    {
        public async Task Send(string message, string userid)
        {
            if (userid != null && userid.Length != 0)
            {
                await Clients.All.SendAsync("receive", message, userid);//응답을 기다리지 않음 리턴값 없음, 순 알림, 브로드캐스트, 명령 실행
            }
            else
            {
                if(userid != null)
                {
                    await Clients.Others.SendAsync("notify", "채팅방에 1명이 퇴장하였습니다.");//보낸 사용자를 제외한 모든 사용자
                }
            }
        }

    }
}
