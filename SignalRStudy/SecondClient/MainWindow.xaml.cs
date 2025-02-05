using Microsoft.AspNetCore.SignalR.Client;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecondClient
{
    public partial class MainWindow : Window
    {
        HubConnection hubConnection;

        public MainWindow()
        {
            InitializeComponent();

            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7247/mychat")
                .WithAutomaticReconnect()//서버랑 연결 끊겨도 자동으로 재연결
                .Build();

            hubConnection.On<string, string>("receive", (message, userid) =>
            {
                //멀티스레드 영역이라 디자인 스레드에서 크로스남 dispatcher은 그걸 막아줌
                Dispatcher.Invoke(() =>
                {
                    chatLog.Items.Add($"{message}의 메세지 {userid}");
                });
            });

            hubConnection.On<string>("notify", (message) =>
            {
                //멀티스레드 영역이라 디자인 스레드에서 크로스남 dispatcher은 그걸 막아줌
                Dispatcher.Invoke(() =>
                {
                    chatLog.Items.Add($"{message}");
                });
            });
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSend.IsEnabled = false;
                await hubConnection.StartAsync();

                chatLog.Items.Add($"안녕하세요. 무엇을 도와드릴까요?");
                btnSend.IsEnabled = true;
            }
            catch (Exception err)
            {
                chatLog.Items.Add($"{err}");
            }
        }
        private async void btnSend_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           await hubConnection.InvokeAsync("Send", textBoxMessage.Text, textBoxUserId.Text);//응답을 기다림, 있음 (Task<T> 반환), 데이터 요청 (DB 조회, 연산 결과 반환 등)
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await hubConnection.InvokeAsync("Send", textBoxMessage.Text + "/"+textBoxUserId.Text, "");

            await hubConnection.StopAsync();
        }
    }
}