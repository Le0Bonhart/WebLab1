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
            string ip = textBox.Text;
            IPAddress? ip_addr = null;

            if (!IPAddress.TryParse(ip, out ip_addr))
            {
                label.Content = "Адрес не корректен";
                return;
            }

            if (ip_addr.AddressFamily != AddressFamily.InterNetwork)
            {
                label.Content = "Неверный протокол";
                return;
            }

            const int port = 5001;

            var tcpEndPoint = new IPEndPoint(ip_addr, port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var message = "change";
            var data = Encoding.UTF8.GetBytes(message);

            try
            {
                tcpSocket.Connect(tcpEndPoint);
                tcpSocket.Send(data);
                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
            }
            catch
            {
                label.Content = "Невозможно подключиться";
            }
        }
    }
}
