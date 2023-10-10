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

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread thread;
        public MainWindow()
        {
            InitializeComponent();

            thread = new Thread(Listen);

            thread.Start();
        }

        public readonly Random global_random = new();

        private bool IsRunning = true;

        public void Listen()
        {
            const string ip = "127.0.0.1";
            const int port = 5001;

            var TcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            TcpSocket.Bind(TcpEndPoint);

            TcpSocket.Listen(1);

            while (IsRunning)
            {
                var listener = TcpSocket.Accept();
                this.Dispatcher.Invoke(RandomizeGrid);
                Thread.Sleep(1000);
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

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsRunning = false;
            thread.Abort();
        }
    }
}
