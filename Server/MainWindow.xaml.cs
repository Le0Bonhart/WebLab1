using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static readonly Random global_random = new();

        private static void RandomizeColor(Rectangle rectangle)
        {
            rectangle.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)global_random.Next(256), (byte)global_random.Next(256), (byte)global_random.Next(256)));
        }

        private void RandomizeGrid()
        {
            
        }
    }
}
