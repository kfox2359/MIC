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
        private int _time = 60;
        private int _startTime = 60;
        private int _wins = 0;
        private bool _timed;

        public MatchingGameScreen(MainWindow window, KinectSensor kinect, bool timed, int time)
        {
            this.kinect = kinect;
            this.window = window;
            _time = time;
            _startTime = time;
            _timed = timed;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;
            if (timed)
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(CountDown);
                timer.Start();
            }
            else
            {
                TimeWord.Visibility = Visibility.Hidden;
            }
        }

        private void CountDown(Object sender, EventArgs args)
        {
            _time--;
            if (_time == 0)
            {
                DispatcherTimer thisTimer = (DispatcherTimer)sender;
                thisTimer.Stop();
            }


            TimeLabel.Content = "" + _time;
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

            if (_wins == 3)
            {
                GameOver();
            }

        }

        private void GameOver()
        {
            /* Made the scores visible on the bottom of the screen */
            congradulations.Visibility = Visibility.Visible;
            finalScore.Visibility = Visibility.Visible;
            finalScore.Content = _score;
            finalScoreText.Visibility = Visibility.Visible;
            if (_timed)
            {
                finalTime.Visibility = Visibility.Visible;
                finalTimeText.Visibility = Visibility.Visible;
                finalTime.Content = "" + (_startTime - _time);
                TimeWord.Visibility = Visibility.Hidden;
                TimeLabel.Visibility = Visibility.Hidden;
            }

            /* Hide the scores on the top */
            scoreTop.Visibility = Visibility.Hidden;
            ScoreLabel.Visibility = Visibility.Hidden;
            //playAgain.Visibility = Visibility.Visible;
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

        private void WrongMatch(Object sender, EventArgs args)
        {
            DispatcherTimer thisTimer = (DispatcherTimer)sender;
            thisTimer.Stop();

            _first.Select();
            _second.Select();
            _first = null;
            _second = null;
        }

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
                line1.Visibility = Visibility.Visible;
                return true;
            }
            if (x1.Sort.Equals("ShortA") && x2.Sort.Equals("ShortA"))
            {
                line3.Visibility = Visibility.Visible;
                return true;
            }
            if (x1.Sort.Equals("Junk") && x2.Sort.Equals("Junk"))
            {
                line2.Visibility = Visibility.Visible;
                return true;
            }

            return false;
        }

    }
}