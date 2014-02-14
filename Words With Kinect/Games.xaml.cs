﻿using Microsoft.Kinect.Toolkit;
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

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class Games : ContentControl
    {
        private KinectSensor kinect;
        public Games(KinectSensor kinect)
        {
            this.kinect = kinect;
            InitializeComponent();
            kinectRegion.KinectSensor = this.kinect;
            InitializeComponent();
        }


       


        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            //This is where that custom code goes
            this.Content = new MemoryGame(kinect);
        }
    }
}
