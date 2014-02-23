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

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for MemoryGameScreen.xaml
    /// </summary>
    public partial class MemoryGameScreen : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;
        private ArrayList cards;

        public MemoryGameScreen(MainWindow window,KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;
            LoadCards();
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MemoryGame(this.window,this.kinect);
        }

        private void KinectTileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadCards()
        {
            //cards.Add(new MemoryCard(R3C1Btn,R3C1Img,0));
        }
    }
}
