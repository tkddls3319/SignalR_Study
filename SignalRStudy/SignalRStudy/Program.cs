using SignalRStudy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();//signalR��� ����
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapHub<MessageHub>("/mychat");//��� ����
app.Run();
