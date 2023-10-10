using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread? thread = null;
        private string ip = "";
        private IPAddress? ip_addr = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        public readonly Random global_random = new();

        private bool IsRunning = true;

        public void Listen()
        {
            const int port = 5001;

            var TcpEndPoint = new IPEndPoint(ip_addr, port);

            var TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                TcpSocket.Bind(TcpEndPoint);
            }
            catch 
            {
                Dispatcher.Invoke(CannotConnect);
                return;
            }

            TcpSocket.Listen(1);

            while (IsRunning)
            {
                var listener = TcpSocket.Accept();
                Dispatcher.Invoke(RandomizeGrid);
            }
        }

        private void RandomizeColor(Rectangle rectangle)
        {
            rectangle.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)global_random.Next(256), (byte)global_random.Next(256), (byte)global_random.Next(256)));
        }

        private void RandomizeGrid()
        {
            foreach (Rectangle rectangle in ColorGrid.Children)
                RandomizeColor(rectangle);
        }

        private void CannotConnect()
        {
            ip_label.Content = "Невозможно подключится по данному адресу";
            return;
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            thread?.Abort();
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            ip = ip_box.Text;

            if (thread != null) return;

            if (!IPAddress.TryParse(ip, out ip_addr))
            {
                ip_label.Content = "Адрес не корректен";
                return;
            }

            if (ip_addr.AddressFamily != AddressFamily.InterNetwork)
            {
                ip_label.Content = "Неверный протокол";
                return;
            }

            ip_box.Text = ip_addr.ToString();

            thread = new Thread(Listen);
            thread.Start();
        }

        private void ip_box_GotFocus(object sender, RoutedEventArgs e)
        {
            ip_label.Content = "Введите IPv4 адрес";
        }
    }
}
