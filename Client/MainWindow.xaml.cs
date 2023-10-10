using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            const string ip = "192.168.94.224";
            const int port = 5001;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var message = "change";
            var data = Encoding.UTF8.GetBytes(message);

            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }
    }
}
