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

using Words_With_Kinect.Matching_Game;
using System.Collections;
using Microsoft.Kinect.Toolkit.Controls;
using System.Threading;
using System.Windows.Threading;

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for MatchingGameScreen.xaml
    /// </summary>
    public partial class MatchingGameScreen : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;
        private bool _flipped = false;
        private int _score = 0;
        private int time = 60;

        public MatchingGameScreen(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MemoryGame(this.window, this.kinect);
        }
    }
}
