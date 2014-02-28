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

using Words_With_Kinect.Memory_Game;
using System.Collections;
using Microsoft.Kinect.Toolkit.Controls;

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

        //Row 1
        private void r1c1_Click(object sender, RoutedEventArgs e)
        {
            r1c1.Flip();
        }

        private void r1c2_Click(object sender, RoutedEventArgs e)
        {
            r1c2.Flip();
        }

        private void r1c3_Click(object sender, RoutedEventArgs e)
        {
            r1c3.Flip();
        }

        private void r1c4_Click(object sender, RoutedEventArgs e)
        {
            r1c4.Flip();
        }
        //Row 2
        private void r2c1_Click(object sender, RoutedEventArgs e)
        {
            r2c1.Flip();
        }

        private void r2c2_Click(object sender, RoutedEventArgs e)
        {
            r2c2.Flip();
        }

        private void r2c3_Click(object sender, RoutedEventArgs e)
        {
            r2c3.Flip();
        }

        private void r2c4_Click(object sender, RoutedEventArgs e)
        {
            r2c4.Flip();
        }
        //Row 3
        private void r3c1_Click(object sender, RoutedEventArgs e)
        {
            r3c1.Flip();
        }

        private void r3c2_Click(object sender, RoutedEventArgs e)
        {
            r3c2.Flip();
        }

        private void r3c3_Click(object sender, RoutedEventArgs e)
        {
            r3c3.Flip();
        }

        private void r3c4_Click(object sender, RoutedEventArgs e)
        {
            r3c4.Flip();
        }



    }
}
