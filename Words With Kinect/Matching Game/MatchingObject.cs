using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Words_With_Kinect.Matching_Game
{
    public class MatchingObject : KinectCircleButton
    {
        private bool _selected=false;
        private bool _disabled = false;
       // private static readonly bool _IsInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register
        (
            "Word",
            typeof(string),
            typeof(MatchingObject),
            new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty LongAProperty = DependencyProperty.Register
        (
             "LongA",
             typeof(bool),
             typeof(MatchingObject),
            new PropertyMetadata()
        );

        public MatchingObject()
        {
            Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE7F7F7"));
            FontSize = 48;
        }

        public string Word
        {
            get
            {
                return(string)GetValue(WordProperty);
            }
            set
            {
                SetValue(WordProperty, value);
            }
        }
        public bool Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                _disabled = value;
            }
        }
        public bool LongA
        {
            get
            {
                return (bool)GetValue(LongAProperty);
            }
            set
            {
                SetValue(LongAProperty, value);
            }
        }

        public void Flip()
        {
            if (!_disabled)
            {
                 //Change the state
                 if (_selected == false)
                {
                     _selected = true;
                     WordState();
                }
                else
                {
                    _selected = false;
                    WordState();
                }
               // ChangeBackground();
            }


        }
        
        /// <summary>
        /// Change the visibility of the words
        /// </summary>
        private void WordState()
        {
            if (_selected)
            {
                Content = Word;
            }
            else
            {
                Content = "";
            }
        }

        /*
        private void ChangeBackground()
        {
            if (_flipped == true)
            {
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF131997"));
            }
            else //Red -- Facedown color
            {
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB61C0D"));
            }
        }*/
    }
}
