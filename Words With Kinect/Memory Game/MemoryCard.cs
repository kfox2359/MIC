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

namespace Words_With_Kinect.Memory_Game
{
    public class MemoryCard : KinectTileButton
    {
        
        
        private bool _flipped=false;
       // private static readonly bool _IsInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register
        (
            "Word",
            typeof(string),
            typeof(MemoryCard),
            new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty LongAProperty = DependencyProperty.Register
        (
             "LongA",
             typeof(bool),
             typeof(MemoryCard),
            new PropertyMetadata()
        );

        public MemoryCard()
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
            
            //Change the state
            if (_flipped == false)
            {
                _flipped = true;
                WordState();
            }
            else
            {
                _flipped = false;
                WordState();
            }
            ChangeBackground();

        }
        /// <summary>
        /// Change the visibility of the words
        /// </summary>
        private void WordState()
        {
            if (_flipped)
            {
                Content = Word;
            }
            else
            {
                Content = "";
            }
        }

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
        }
    }
}
