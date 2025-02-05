using SignalRStudy;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR().AddHubOptions<MessageHub>(options =>
{
    options.EnableDetailedErrors = true;//30초가 기본 값, 
    options.ClientTimeoutInterval = System.TimeSpan.FromSeconds(30);//30초 지나면 서버에서 클라 끊음
});


var app = builder.Build();
app.UseDefaultFiles();//www루트 폴더 url생성
app.UseStaticFiles();//적용


app.MapGet("/", () => "Hello World!");
app.MapHub<MessageHub>("/mychat");//허브 연결
app.Run();

