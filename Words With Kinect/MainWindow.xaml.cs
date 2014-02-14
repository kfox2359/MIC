using Microsoft.Kinect;
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

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        
        private KinectSensorChooser sensorChooser;
        private bool nearMode;
        public MainWindow(bool nearMode)
        {
            this.nearMode = nearMode;
            InitializeComponent();
            Loaded += OnLoaded;
            InitializeComponent();
        }

        #region BoilerPlate Code for the kinect 


        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
        }
        /// <summary>
        /// Takes care of when a new kinect comes in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            bool error = false;
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                    error = true;
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        if (nearMode == true)
                        {
                            args.NewSensor.DepthStream.Range = DepthRange.Near;
                            args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                            args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                        }
                        else
                        {
                            args.NewSensor.DepthStream.Range = DepthRange.Default;
                            args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                            args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                        }

                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
            if (!error)
                kinectRegion.KinectSensor = args.NewSensor;

        }
        #endregion


        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
           // this.sensorChooser.Kinect.Stop();
            this.Content = new Games(sensorChooser.Kinect);
           // this.sensorChooser.Stop();
            /* this about passing the kinect as a parameter to the next class, that way we
             * do not have to worry about resetting the kinect up. Also thing about making the boilerplate code
             * into a seperate class */
        }
    }
}
