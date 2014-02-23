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
        private KinectTileButton firstBtn;
        private KinectTileButton secondBtn;
        private Image firstImg;
        private Image secondImg;
        private string firstImgName;
        private string secondImgName;
        private bool selected = false;

        public MemoryGameScreen(MainWindow window,KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;
            Init();
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MemoryGame(this.window,this.kinect);
        }

        private bool IsWinner()
        {
            
            return firstImgName == secondImgName;
        }

        private void ProcessMatch(bool winner)
        {
            if(!winner)
            {
                firstBtn.Visibility = Visibility.Visible;
                firstImg.Visibility = Visibility.Hidden;
                secondBtn.Visibility = Visibility.Visible;
                secondImg.Visibility = Visibility.Hidden;
            }
            else
            {
               int temp = Convert.ToInt32(Score.Text);
               temp += 10;
               Score.Text = "" + temp;
            }
        }
        private void Init()
        {
            R3C1Img.Visibility = Visibility.Hidden;
            R2C1Img.Visibility = Visibility.Hidden;
        }

        /* Ball */
        private void R2C1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (selected == false)
            {
                R2C1Btn.Visibility = Visibility.Hidden;
                R2C1Img.Visibility = Visibility.Visible;
                firstBtn = R2C1Btn;
                firstImg = R2C1Img;
                firstImgName = "Ball";
                selected = true;
            }
            else
            {
                R2C1Btn.Visibility = Visibility.Hidden;
                R2C1Img.Visibility = Visibility.Visible;
                secondBtn = R2C1Btn;
                secondImg = R2C1Img;
                secondImgName = "Ball";
                selected = false;
                ProcessMatch(IsWinner());
            }
        }
        /* Ball */
        private void R3C1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (selected == false)
            {
                R3C1Btn.Visibility = Visibility.Hidden;
                R3C1Img.Visibility = Visibility.Visible;
                firstBtn = R3C1Btn;
                firstImg = R3C1Img;
                firstImgName = "Ball";
                selected = true;
            }
            else
            {
                R3C1Btn.Visibility = Visibility.Hidden;
                R3C1Img.Visibility = Visibility.Visible;
                secondBtn = R3C1Btn;
                secondImg = R3C1Img;
                secondImgName = "Ball";
                selected = false;
                ProcessMatch(IsWinner());
            }

        }
    }
}
