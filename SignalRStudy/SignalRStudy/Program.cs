using SignalRStudy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();//signalR사용 정의
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapHub<MessageHub>("/mychat");//허브 연결
app.Run();
