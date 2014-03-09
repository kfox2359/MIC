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
    /// Interaction logic for WordSortGameScreen.xaml
    /// </summary>
    public partial class WordSortGameScreen : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;
        public WordSortGameScreen(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            kinectRegion.KinectSensor = kinect;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            window.Content = new WordSort(window, kinect);
        }
        private double columnLeft(String columnType)
        {
            if (columnType == "LongA")
            {
                return Canvas.GetLeft(LongA_Column);
            }
            else if (columnType == "ShortA")
            {
                return Canvas.GetLeft(ShortA_Column);
            }
            else return Canvas.GetLeft(Oddball_Column);
        }
        private double columnTop(String columnType)
        {
            if (columnType == "LongA")
            {
                return Canvas.GetTop(LongA_Column);
            }
            else if (columnType == "ShortA")
            {
                return Canvas.GetTop(ShortA_Column);
            }
            else return Canvas.GetTop(Oddball_Column);
        }
    }
}
