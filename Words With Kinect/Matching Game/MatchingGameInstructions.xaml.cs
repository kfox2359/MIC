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
    /// Interaction logic for MatchingGameInstructions.xaml
    /// </summary>
    public partial class MatchingGameInstructions : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;

        public MatchingGameInstructions(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            kinectRegion.KinectSensor = this.kinect;
        }

     /// <summary>
        /// Open the actual game screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton(object sender, RoutedEventArgs e)
        {
            bool selected = (bool)timedBox.IsChecked;
            int sec = 0;
            try
            {
                sec = Convert.ToInt32(seconds.Text);
            }
            catch (Exception) { }

            this.window.Content = new MatchingGameScreen(this.window, kinect, selected, sec);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.window.Content = new Games(this.window,kinect);
        }

        private void timedBox_Checked(object sender, RoutedEventArgs e)
        {
            seconds.Visibility = Visibility.Visible;
        }
    }
}
