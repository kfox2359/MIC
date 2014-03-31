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
    public class MatchingObject : KinectTileButton
    {
        private bool _selected=false;
        private bool _disabled = false;

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register
        (
            "Word",
            typeof(string),
            typeof(MatchingObject),
            new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty SortProperty = DependencyProperty.Register
        (
             "Sort",
             typeof(string),
             typeof(MatchingObject),
             new PropertyMetadata(string.Empty)
        );

        public MatchingObject()
        {

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

        public string Sort
        {
            get
            {
                return (string)GetValue(SortProperty);
            }
            set
            {
                SetValue(SortProperty, value);
            }
        }

        public void Select()
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
               //unhighlight
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
        private void highlight()
        {
            if (_selected == true)
            {
                
            }
        }
        */
    }
}
