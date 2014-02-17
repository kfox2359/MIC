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
    /// Interaction logic for MemoryGame.xaml
    /// </summary>
    public partial class MemoryGame : ContentControl
    {
        private KinectSensor kinect;

        public MemoryGame(KinectSensor kinect)
        {
            this.kinect = kinect;
            InitializeComponent();
            kinectRegion.KinectSensor = kinect;
            InitializeComponent();
        }

        /// <summary>
        /// Open the actual game screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MemoryGameScreen(kinect);
        }

        private void CustomButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Content = new Games(kinect);
        }
    }
}
