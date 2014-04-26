﻿using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect.Toolkit.Controls;
using System.Windows;
using System.Windows.Media;

namespace Words_With_Kinect.Word_Sort_Game
{
    public class DragButton : KinectTileButton
    {
        private HandPointer _capturedHandPointer;
        private bool isGripped = false;
        public enum wordType{LongA, ShortA, OddBall};

        public static readonly DependencyProperty wordTypeProperty = DependencyProperty.Register
       (
            "wordType",
            typeof(wordType),
            typeof(DragButton),
           new PropertyMetadata()
       );

        public wordType type
        {
            get
            {
                return (wordType)GetValue(wordTypeProperty);
            }
            set
            {
                SetValue(wordTypeProperty, value);
            }
        }

        public DragButton()
        {
            Initialise();
            Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE7F7F7"));
        }

        public DragButton(String word, wordType type)
        {
            Initialise();
            Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000"));
            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE7F7F7"));
            this.Content = word;
            this.type = type;
        }
        private void Initialise()
        {
            KinectRegion.AddHandPointerGotCaptureHandler(this, this.OnHandPointerCaptured);
            KinectRegion.AddHandPointerLostCaptureHandler(this, this.OnHandPointerLostCapture);
            KinectRegion.AddHandPointerGripHandler(this, this.OnHandPointerGrip);
            KinectRegion.AddHandPointerMoveHandler(this, this.OnHandPointerMove);
            KinectRegion.AddHandPointerGripReleaseHandler(this, this.OnHandPointerGripRelease);
            KinectRegion.AddQueryInteractionStatusHandler(this, this.OnQueryInteractionStatus);

            KinectRegion.SetIsGripTarget(this, true);
            this.AllowDrop = true;
        }

        private void OnQueryInteractionStatus(object sender, QueryInteractionStatusEventArgs e)
        {
            if (this.Equals(e.HandPointer.Captured))
            {
                e.IsInGripInteraction = this.isGripped;
                e.Handled = true;
            }
        }

        private void OnHandPointerLostCapture(object sender, HandPointerEventArgs e)
        {
            if (_capturedHandPointer == e.HandPointer)
            {
                _capturedHandPointer = null;
                IsPressed = false;
                e.Handled = true;
            }
        }

        private void OnHandPointerCaptured(object sender, HandPointerEventArgs e)
        {
            if (_capturedHandPointer == null)
            {
                _capturedHandPointer = e.HandPointer;
                IsPressed = true;
                e.Handled = true;
            }
        }

        private void OnHandPointerGrip(object sender, HandPointerEventArgs e)
        {
            if (e.HandPointer.IsPrimaryUser && e.HandPointer.IsPrimaryHandOfUser && e.HandPointer.IsInteractive)
            {
                if (e.HandPointer == null)
                {
                    return;
                }
                if (this._capturedHandPointer != e.HandPointer)
                {
                    if (e.HandPointer.Captured == null)
                    {
                        // Only capture hand pointer if it isn't already captured
                        e.HandPointer.Capture(this);
                    }
                    else
                    {
                        // Some other control has capture, ignore grip
                        return;
                    }
                }
                this.isGripped = true;
                e.Handled = true;
            }
        }

        private void OnHandPointerMove(object sender, HandPointerEventArgs e)
        {
            if (this.Equals(e.HandPointer.Captured))
            {
                e.Handled = true;

                var curpos = e.HandPointer.GetPosition(this);

                if (!this.isGripped)
                {
                    return;
                }
                var buttonleft = Canvas.GetLeft(this);
                var buttontop = Canvas.GetTop(this);

                Canvas.SetLeft(this, buttonleft + curpos.X);
                Canvas.SetTop(this, buttontop + curpos.Y);
            }
        }

        private void OnHandPointerGripRelease(object sender, HandPointerEventArgs e)
        {
            this.isGripped = false;
            e.HandPointer.Capture(null);
            e.Handled = true;
        }
    }
}
