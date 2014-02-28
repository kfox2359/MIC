using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Kinect;

namespace Words_With_Kinect.Word_Sort_Game
{
    /// <summary>
    /// Interaction logic for WordSort.xaml
    /// </summary>

    public partial class WordSort : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;

        public WordSort(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            kinectRegion.KinectSensor = kinect;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            window.Content = new WordSortGameScreen(window, kinect);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            window.Content = new Games(window, kinect);
        }
    }
}
