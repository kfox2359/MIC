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
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using Microsoft.Kinect;
using System.IO;

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class Games : UserControl
    {
        private KinectSensor kinect;
        private MainWindow window;

        SpeechSynthesizer reader = new SpeechSynthesizer();

        /// <summary>
        /// Active Kinect sensor.
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;

        public Games(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            kinectRegion.KinectSensor = this.kinect;
            this.Loaded += WindowLoaded;
            // InitializeComponent();
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            this.window.Content = new MemoryGame(this.window, kinect);
        }

        private void WordSortClick(object sender, RoutedEventArgs e)
        {
            window.Content = new Word_Sort_Game.WordSort(window, kinect);
        }

        private void MatchingGameClick(object sender, RoutedEventArgs e)
        {
            window.Content = new MatchingGameInstructions(window, kinect);
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
                // directions.Add(new SemanticResultValue("computer start", "START"));
                directions.Add(new SemanticResultValue("computer start Memory game", "MEMORY GAME"));
                directions.Add(new SemanticResultValue("lets start Memory game", "MEMORY GAME"));
                directions.Add(new SemanticResultValue("lets play Memory game", "MEMORY GAME"));
                directions.Add(new SemanticResultValue("computer start Memory", "MEMORY GAME"));
                directions.Add(new SemanticResultValue("computer start matching", "MATCHING GAME"));
                directions.Add(new SemanticResultValue("lets start matching game", "MATCHING GAME"));
                directions.Add(new SemanticResultValue("lets play matching game", "MATCHING GAME"));
                directions.Add(new SemanticResultValue("computer start matching game", "MATCHING GAME"));
                directions.Add(new SemanticResultValue("computer start word sort game", "WORD SORT GAME"));
                directions.Add(new SemanticResultValue("lets start word sort game", "WORD SORT GAME"));
                directions.Add(new SemanticResultValue("lets play word sort game", "WORD SORT GAME"));
                directions.Add(new SemanticResultValue("computer start word sort", "WORD SORT GAME"));
                //Console.Write("directions.Add(new SemanticResultValue(\"start\", \"START\"));");
                //directions.Add(new SemanticResultValue("computer lets start", "START"));
                var gb = new GrammarBuilder { Culture = ri.Culture };
                gb.Append(directions);

                var g = new Grammar(gb);

                speechEngine.LoadGrammar(g);
                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                //reader.SpeakAsync("loading speech engine.");
                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                ////speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                speechEngine.SetInputToAudioStream(
                    sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }

            //reader.SpeakAsync("loading finished.");

        }
        void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {

            Console.Write("this.Content = new Games(this, sensorChooser.Kinect);");
        }

        /// <summary>
        /// Handler for recognized speech events.
        /// </summary>              
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.8;


            if (e.Result.Confidence >= ConfidenceThreshold)
            //if (e.Result.Semantics.Value.ToString() != null && !e.Result.Semantics.Value.ToString().Equals(""))
            {
                if (e.Result.Semantics.Value.ToString() != "")
                {
                }
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "MATCHING GAME":
                        this.window.Content = new MatchingGameInstructions(window, kinect);
                        reader.SpeakAsync("Start matching game!");
                        break;
                    case "WORD SORT GAME":
                        this.window.Content = new Word_Sort_Game.WordSort(window, kinect);
                        reader.SpeakAsync("Start word sort game!");

                        break;
                    case "MEMORY GAME":
                        this.window.Content = new MemoryGame(this.window, kinect);
                        reader.SpeakAsync("Start memory game!");
                        break;
                        
                }
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
