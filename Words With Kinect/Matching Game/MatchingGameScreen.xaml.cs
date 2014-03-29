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

        public MatchingGameScreen(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            this.kinectRegion.KinectSensor = kinect;

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MatchingGameInstructions(this.window, this.kinect);
        }

        private void Match1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Match2(object sender, RoutedEventArgs e)
        {

        }

        private void Match3(object sender, RoutedEventArgs e)
        {

        }


        private void WrongMatch(Object sender, EventArgs args)
        {
            _first.Select();
            _second.Select();
            _first = null;
            _second = null;
        }

        /*
         * Match events
         */

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
        /// <param name="pic1"></param>
        /// <param name="pic2"></param>
        /// <returns></returns>
        private bool CorrectMatch(MatchingObject x1, MatchingObject x2)
        {
            if (x1.LongA.Equals("LongA") && x2.LongA.Equals("LongA"))
            {
                return true;
            }
            if (x1.LongA.Equals("ShortA") && x2.LongA.Equals("ShortA"))
            {
                return true;
            }
            if (x1.LongA.Equals("Junk") && x2.LongA.Equals("Junk"))
            {
                return true;
            }

            return false;
        }
      
    }
}
