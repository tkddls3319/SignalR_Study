using SignalRStudy;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR().AddHubOptions<MessageHub>(options =>
{
    options.EnableDetailedErrors = true;//30�ʰ� �⺻ ��, 
    options.ClientTimeoutInterval = System.TimeSpan.FromSeconds(30);//30�� ������ �������� Ŭ�� ����
});


var app = builder.Build();
app.UseDefaultFiles();//www��Ʈ ���� url����
app.UseStaticFiles();//����


app.MapGet("/", () => "Hello World!");
app.MapHub<MessageHub>("/mychat");//��� ����
app.Run();

