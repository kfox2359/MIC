using Microsoft.Kinect.Toolkit;
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

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for MemoryGameScreen.xaml
    /// </summary>
    public partial class MemoryGameScreen : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;
   
        public MemoryGameScreen(MainWindow window,KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MemoryGame(this.window,this.kinect);
        }

        private void KinectTileButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
