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
        private bool _selected = false;
        private MatchingObject _first;
        private MatchingObject _second;
        private int _score = 0;
        private int time = 60;
        private int _startTime = 60;
        private int _wins = 0;
        private bool _timed;

        public MatchingGameScreen(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;

        }

        //---------------------BUTTONS---------------------
        private void Back(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MatchingGameInstructions(this.window, this.kinect);
        }

        //Column 1
        private void r1c1_button(object sender, RoutedEventArgs e)
        {
            r1c1.Select();
            CheckWinner(RegisterSelection(r1c1));
        }

        private void r2c1_button(object sender, RoutedEventArgs e)
        {
            r2c1.Select();
            CheckWinner(RegisterSelection(r2c1));
        }

        private void r3c1_button(object sender, RoutedEventArgs e)
        {
            r3c1.Select();
            CheckWinner(RegisterSelection(r3c1));
        }

        //Column 2
        private void r1c2_button(object sender, RoutedEventArgs e)
        {
            r1c2.Select();
            CheckWinner(RegisterSelection(r1c2));
        }

        private void r2c2_button(object sender, RoutedEventArgs e)
        {
            r2c2.Select();
            CheckWinner(RegisterSelection(r2c2));
        }

        private void r3c2_button(object sender, RoutedEventArgs e)
        {
            r3c2.Select();
            CheckWinner(RegisterSelection(r3c2));
        }


        //---------------------Functions---------------------
        

        /// <summary>
        /// Registers when there is a selection and returns if there is a correct match
        /// </summary>
        /// <param name="pic"></param>
        /// <returns></returns>
        private bool RegisterSelection(MatchingObject x)
        {
            if (!x.Disabled)
            {
                if (_selected == false)
                {
                    _selected = true;
                    _first = x;
                    return false;
                }

                _selected = false;
                _second = x;
                return true;
            }
            return false;
        }



        private void ProcessLoss()
        {
            _score -= 4;
            ScoreLabel.Content = "" + _score;
        }
        private void ProcessWin()
        {
            _score += 10;
            ScoreLabel.Content = "" + _score;
            _wins += 1;

            if (_wins == 6)
            {
                //GameOver();
            }
                
        }


        private void WrongMatch(Object sender, EventArgs args)
        {
            _first.Select();
            _second.Select();
            _first = null;
            _second = null;
        }

        /// <summary>
        /// Returns if the objects selected are correctly matched
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        private bool Match(MatchingObject x1, MatchingObject x2)
        {
            if (x1.Sort.Equals("LongA") && x2.Sort.Equals("LongA"))
            {
                // Create a Line
                Line redLine = new Line();
                redLine.X1 = 200;
                redLine.Y1 = 200;
                redLine.X2 = 400;
                redLine.Y2 = 400;

                // Create a red Brush
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Red;

                // Set Line's width and color
                redLine.StrokeThickness = 4;
                redLine.Stroke = redBrush;

                return true;
            }
            if (x1.Sort.Equals("ShortA") && x2.Sort.Equals("ShortA"))
            {
                // Create a Line
                Line redLine = new Line();
                redLine.X1 = 200;
                redLine.Y1 = 200;
                redLine.X2 = 400;
                redLine.Y2 = 200;

                // Create a red Brush
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Red;

                // Set Line's width and color
                redLine.StrokeThickness = 4;
                redLine.Stroke = redBrush;
                return true;
            }
            if (x1.Sort.Equals("Junk") && x2.Sort.Equals("Junk"))
            {
                // Create a Line
                Line redLine = new Line();
                redLine.X1 = 200;
                redLine.Y1 = 400;
                redLine.X2 = 400;
                redLine.Y2 = 200;

                // Create a red Brush
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Red;

                // Set Line's width and color
                redLine.StrokeThickness = 4;
                redLine.Stroke = redBrush;
                return true;
            }

            return false;
        }

        private void CheckWinner(bool winCon)
        {
            if (winCon)
            {
                if (Match(_first, _second))
                {
                    _first.Disabled = true;
                    _second.Disabled = true;
                    ProcessWin();
                }
                else
                {
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(.5);
                    timer.Tick += new EventHandler(WrongMatch);
                    timer.Start();
                    ProcessLoss();
                }
            }
        }
      
    }
}
