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
        private KinectSensor kinect;
        private MainWindow window;
        private bool _flipped = false;
        private MemoryCard _first;
        private MemoryCard _second;
        private int _score=0;

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
    }
}
