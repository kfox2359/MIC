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
using System.Threading;
using System.Windows.Threading;

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for MemoryGameScreen.xaml
    /// </summary>
    public partial class MemoryGameScreen : UserControl
    {
        private KinectSensor _kinect;
        private MainWindow _window;
        private bool _flipped = false;
        private MemoryCard _first;
        private MemoryCard _second;
        private int _score = 0;
        private int _time = 60;
        private int _startTime = 60;
        private int _wins = 0;
        private bool _timed;

        public MemoryGameScreen(MainWindow window,KinectSensor kinect, bool timed, int time)
        {
            this._kinect = kinect;
            this._window = window;
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

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            this._window.Content = new MemoryGame(this._window,this._kinect);
        }

        #region Memory Card Click Events
        //Row 1
        private void r1c1_Click(object sender, RoutedEventArgs e)
        {
            r1c1.Flip();
            
            CheckWinner(RegisterFlip(r1c1));
        }

        private void r1c2_Click(object sender, RoutedEventArgs e)
        {
            r1c2.Flip();
            
            CheckWinner(RegisterFlip(r1c2));
        }

        private void r1c3_Click(object sender, RoutedEventArgs e)
        {
            r1c3.Flip();
            
            CheckWinner(RegisterFlip(r1c3));
        }

        private void r1c4_Click(object sender, RoutedEventArgs e)
        {
            r1c4.Flip();
            
            CheckWinner(RegisterFlip(r1c4));
        }
        //Row 2
        private void r2c1_Click(object sender, RoutedEventArgs e)
        {
            r2c1.Flip();
            
            CheckWinner(RegisterFlip(r2c1));
        }

        private void r2c2_Click(object sender, RoutedEventArgs e)
        {
            r2c2.Flip();
            
            CheckWinner(RegisterFlip(r2c2));
        }

        private void r2c3_Click(object sender, RoutedEventArgs e)
        {
            r2c3.Flip();
            
            CheckWinner(RegisterFlip(r2c3));
        }

        private void r2c4_Click(object sender, RoutedEventArgs e)
        {
            r2c4.Flip();
            
            CheckWinner(RegisterFlip(r2c4));
        }
        //Row 3
        private void r3c1_Click(object sender, RoutedEventArgs e)
        {
            r3c1.Flip();
            
            CheckWinner(RegisterFlip(r3c1));
        }

        private void r3c2_Click(object sender, RoutedEventArgs e)
        {
            r3c2.Flip();
            
            CheckWinner(RegisterFlip(r3c2));
        }

        private void r3c3_Click(object sender, RoutedEventArgs e)
        {
            r3c3.Flip();
            
            CheckWinner(RegisterFlip(r3c3));
        }

        private void r3c4_Click(object sender, RoutedEventArgs e)
        {
            r3c4.Flip();
            
            CheckWinner(RegisterFlip(r3c4));
        }
        #endregion

        /// <summary>
        /// Takes care of the win condition
        /// </summary>
        /// <param name="winCon"></param>
        private void CheckWinner(bool winCon)
        {
            if (winCon)
            {
                if (IsWinner(_first, _second))
                {
                    _first.Disabled = true;
                    _second.Disabled = true;
                    ProcessWin();
                }
                else
                {
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(.5);
                    timer.Tick += new EventHandler(LoseFlip);
                    timer.Start();
                    ProcessLoss();
                }
            }

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
                GameOver();
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
                finalTime.Content = ""+ (_startTime - _time);
                TimeWord.Visibility = Visibility.Hidden;
                TimeLabel.Visibility = Visibility.Hidden;
            }

            /* Hide the scores on the top */
            scoreTop.Visibility = Visibility.Hidden;
            ScoreLabel.Visibility = Visibility.Hidden;
            playAgain.Visibility = Visibility.Visible;
        }

        private void LoseFlip(Object sender, EventArgs args)
        {
            DispatcherTimer thisTimer = (DispatcherTimer)sender;
            thisTimer.Stop();
            
            _first.Flip();
            _second.Flip();
            _first = null;
            _second = null;
        }

        /// <summary>
        /// Registers when there is a flip and returns if there is a win condition
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool RegisterFlip(MemoryCard card)
        {
            if (!card.Disabled)
            {
            if (_flipped == false)
            {
                _flipped = true;
                _first = card;
               return false;
            }

            _flipped = false;
            _second = card;
            return true;
            }
            return false;

        }

        /// <summary>
        /// Returns if the cards flipped produced a winner
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        private bool IsWinner(MemoryCard card1, MemoryCard card2)
        {
            return card1.LongA == card2.LongA;
        }

        private void playAgain_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = new MemoryGame(_window, _kinect);
        }
    }
}
