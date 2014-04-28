﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using Microsoft.Kinect;
namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for StartupOptions.xaml
    /// @Author Kevin Fox
    /// </summary>
    public partial class StartupOptions : Window
    {

        SpeechSynthesizer reader = new SpeechSynthesizer();
        private bool nearMode;

        private MainWindow window;
        /// <summary>
        /// Active Kinect sensor.
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;
        public StartupOptions()
        {
            InitializeComponent();
            reader = new SpeechSynthesizer();
            //reader.SpeakAsync("Please choose to enable or disenable near mode");
            this.Loaded += this.WindowLoaded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool nearMode =false;

            if (RadioBtnEnable.IsChecked == true)
            {
                nearMode = true;
            }
            if (RadioBtnDisable.IsChecked == true)
            {
                nearMode = false;
            }
            MainWindow app = new MainWindow(nearMode);
            App.Current.MainWindow = app;
            this.Close();
            
            app.Show();
            
           
        }


        /// <summary>
        /// Gets the metadata for the speech recognizer (acoustic model) most suitable to
        /// process audio from Kinect device.
        /// </summary>
        /// <returns>
        /// RecognizerInfo if found, <code>null</code> otherwise.
        /// </returns>
        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }


        /// <summary>
        /// Execute initialization tasks.
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                try
                {
                    // Start the sensor!
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    // Some other application is streaming from the same Kinect sensor
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                //nothing
                return;
            }

            RecognizerInfo ri = GetKinectRecognizer();

            if (null != ri)
            {

                this.speechEngine = new SpeechRecognitionEngine(ri.Id);


                //Use this code to create grammar programmatically rather than from
                //a grammar file.

                var directions = new Choices();
                directions.Add(new SemanticResultValue("enable near mode", "ENABLE NEAR MODE"));
                directions.Add(new SemanticResultValue("disable near mode", "DISABLE NEAR MODE"));
                directions.Add(new SemanticResultValue("please enable near mode", "ENABLE NEAR MODE"));
                directions.Add(new SemanticResultValue("please disable near mode", "DISABLE NEAR MODE"));
                //Console.Write("directions.Add(new SemanticResultValue(\"start\", \"START\"));");
                //directions.Add(new SemanticResultValue("computer lets start", "START"));
                var gb = new GrammarBuilder { Culture = ri.Culture };
                gb.Append(directions);

                var g = new Grammar(gb);

                speechEngine.LoadGrammar(g);
                /***
                // Create a grammar from grammar definition XML file.
                using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.SpeechGrammar)))
                {
                    var g = new Grammar(memoryStream);
                    speechEngine.LoadGrammar(g);
                }
                ***/
                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                //reader.SpeakAsync("loading speech engine.");
                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                ////speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                speechEngine.SetInputToAudioStream(
                    sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
                //speechEngine.RecognizeAsync();
            }

            // reader.SpeakAsync("loading finished.");

        }
        void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {

        }


        /// <summary>
        /// Handler for recognized speech events.
        /// </summary>              
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.7;

            if (e.Result.Confidence >= ConfidenceThreshold)
            //if (e.Result.Semantics.Value.ToString() != null && !e.Result.Semantics.Value.ToString().Equals(""))
            {


                switch (e.Result.Semantics.Value.ToString())
                {
                    case "ENABLE NEAR MODE":
                        nearMode = true;
                        break;

                    case "DISABLE NEAR MODE":
                        nearMode = false;
                        break;
                }

                MainWindow app = new MainWindow(nearMode);
                App.Current.MainWindow = app;
                this.Close();

                app.Show();
            }
        }


        /// <summary>
        /// Handler for rejected speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            //ClearRecognitionHighlights();
        }
    }
}
