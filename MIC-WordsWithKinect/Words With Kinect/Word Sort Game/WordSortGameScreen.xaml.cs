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
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace Words_With_Kinect.Word_Sort_Game
{
    /// <summary>
    /// Interaction logic for WordSortGameScreen.xaml
    /// </summary>
    public partial class WordSortGameScreen : UserControl
    {
        SpeechSynthesizer reader = new SpeechSynthesizer();

        private KinectSensor kinect;
        private MainWindow window;
        /// <summary>
        /// Active Kinect sensor.
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;

        private int numCorrect = 0;
        public WordSortGameScreen(MainWindow window, KinectSensor kinect)
        {
            this.kinect = kinect;
            this.window = window;
            InitializeComponent();
            kinectRegion.KinectSensor = kinect;
            //load
            this.Loaded += this.WindowLoaded;

            WordDatabaseDataSet.WordsTableDataTable wt = new WordDatabaseDataSet.WordsTableDataTable();
            WordDatabaseDataSetTableAdapters.WordsTableTableAdapter tableAdapter = 
                new WordDatabaseDataSetTableAdapters.WordsTableTableAdapter();

            wt = tableAdapter.GetData();
            DataRow[] rows = wt.Select();

            Random rand = new Random();
            int numwords = 0;

            DragButton[] words = new DragButton[8];
            DataRow row;
            DragButton.wordType type = DragButton.wordType.OddBall;

            while (numwords < 8)
            {
                int r = rand.Next(0, rows.Length);
                row = rows[r];
                if (row == null)
                {
                    continue;
                }
                rows[r] = null;

                if (row["WordSound"].Equals("LongA"))
                {
                    type = DragButton.wordType.LongA;
                }
                else if (row["WordSound"].Equals("ShortA"))
                {
                    type = DragButton.wordType.ShortA;
                }
                DragButton db = new DragButton(row["Word"].ToString(), type);
                    
                words[numwords++] = db;
            }

            this.word_one.Content = words[0].Content;
            this.word_one.type = words[0].type;
            this.word_two.Content = words[1].Content;
            this.word_two.type = words[1].type;
            this.word_three.Content = words[2].Content;
            this.word_three.type = words[2].type;
            this.word_four.Content = words[3].Content;
            this.word_four.type = words[3].type;
            this.word_five.Content = words[4].Content;
            this.word_five.type = words[4].type;
            this.word_six.Content = words[5].Content;
            this.word_six.type = words[5].type;
            this.word_seven.Content = words[6].Content;
            this.word_seven.type = words[6].type;
            this.word_eight.Content = words[7].Content;
            this.word_eight.type = words[7].type;
            

            //foreach(DataRow row in rows)
            //{
                //Console.WriteLine(row["Word"]);
            //}
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            window.Content = new WordSort(window, kinect);
        }

        private double columnLeft(String columnType)
        {
            if (columnType.Equals("LongA"))
            {
                return Canvas.GetLeft(LongA_Column);
            }
            else if (columnType.Equals("ShortA"))
            {
                return Canvas.GetLeft(ShortA_Column);
            }
            else return Canvas.GetLeft(Oddball_Column);
        }

        private double columnTop(String columnType)
        {
            if (columnType.Equals("LongA"))
            {
                return Canvas.GetTop(LongA_Column);
            }
            else if (columnType.Equals("ShortA"))
            {
                return Canvas.GetTop(ShortA_Column);
            }
            else return Canvas.GetTop(Oddball_Column);
        }

        private double columnWidth(String columnType)
        {
            if (columnType.Equals("LongA"))
            {
                return LongA_Column.ActualWidth;
            }
            else if (columnType.Equals("ShortA"))
            {
                return ShortA_Column.ActualWidth;
            }
            else return Oddball_Column.ActualWidth;
        }

        private double columnHeight(String columnType)
        {
            if (columnType.Equals("LongA"))
            {
                return LongA_Column.ActualHeight;
            }
            else if (columnType.Equals("ShortA"))
            {
                return ShortA_Column.ActualHeight;
            }
            else return Oddball_Column.ActualHeight;
        }

        private async void DragButton_Drop(object sender, RoutedEventArgs e)
        {
            DragButton button = (DragButton)sender;
            double top = Canvas.GetTop(button) + button.ActualHeight/2;
            double left = Canvas.GetLeft(button) + button.ActualWidth/2;

            switch (button.type)
            {
                case DragButton.wordType.LongA:
                    if (isOver("LongA", top, left))
                    {
                        LongA_Column.BorderBrush = Brushes.Green;
                        LongA_Column.Foreground = Brushes.Green;
                        button.Visibility = Visibility.Hidden;
                        numCorrect++;
                        await Task.Delay(1000);
                        LongA_Column.BorderBrush = Brushes.White;
                        LongA_Column.Foreground = Brushes.White;
                    }
                    else if (isOver("ShortA", top, left))
                    {
                        ShortA_Column.BorderBrush = Brushes.Red;
                        ShortA_Column.Foreground = Brushes.Red;
                        await Task.Delay(1000);
                        ShortA_Column.BorderBrush = Brushes.White;
                        ShortA_Column.Foreground = Brushes.White;
                    }
                    else if (isOver("OddBall", top, left))
                    {
                        Oddball_Column.BorderBrush = Brushes.Red;
                        Oddball_Column.Foreground = Brushes.Red;
                        await Task.Delay(1000);
                        Oddball_Column.BorderBrush = Brushes.White;
                        Oddball_Column.Foreground = Brushes.White;
                    }
                    break;
                case DragButton.wordType.ShortA:
                    if (isOver("LongA", top, left))
                    {
                        LongA_Column.BorderBrush = Brushes.Red;
                        LongA_Column.Foreground = Brushes.Red;
                        await Task.Delay(1000);
                        LongA_Column.BorderBrush = Brushes.White;
                        LongA_Column.Foreground = Brushes.White;
                    }
                    else if (isOver("ShortA", top, left))
                    {
                        ShortA_Column.BorderBrush = Brushes.Green;
                        ShortA_Column.Foreground = Brushes.Green;
                        button.Visibility = Visibility.Hidden;
                        numCorrect++;
                        await Task.Delay(1000);
                        ShortA_Column.BorderBrush = Brushes.White;
                        ShortA_Column.Foreground = Brushes.White;
                    }
                    else if (isOver("OddBall", top, left))
                    {
                        Oddball_Column.BorderBrush = Brushes.Red;
                        Oddball_Column.Foreground = Brushes.Red;
                        await Task.Delay(1000);
                        Oddball_Column.BorderBrush = Brushes.White;
                        Oddball_Column.Foreground = Brushes.White;
                    }
                    break;
                case DragButton.wordType.OddBall:
                    if (isOver("LongA", top, left))
                    {
                        LongA_Column.BorderBrush = Brushes.Red;
                        LongA_Column.Foreground = Brushes.Red;
                        await Task.Delay(1000);
                        LongA_Column.BorderBrush = Brushes.White;
                        LongA_Column.Foreground = Brushes.White;
                    }
                    else if (isOver("ShortA", top, left))
                    {
                        ShortA_Column.BorderBrush = Brushes.Red;
                        ShortA_Column.Foreground = Brushes.Red;
                        await Task.Delay(1000);
                        ShortA_Column.BorderBrush = Brushes.White;
                        ShortA_Column.Foreground = Brushes.White;
                    }
                    else if (isOver("OddBall", top, left))
                    {
                        Oddball_Column.BorderBrush = Brushes.Green;
                        Oddball_Column.Foreground = Brushes.Green;
                        button.Visibility = Visibility.Hidden;
                        numCorrect++;
                        await Task.Delay(1000);
                        Oddball_Column.BorderBrush = Brushes.White;
                        Oddball_Column.Foreground = Brushes.White;
                    }
                    break;
                default:
                    Console.WriteLine("Null or invalid type assigned to this word.");
                    break;
            }
            if (numCorrect == 8)
            {
                window.Content = new WordSort(window, kinect);
            }
        }

        private bool isOver(string columnType, double top, double left)
        {
            double colTop = columnTop(columnType);
            double colLeft = columnLeft(columnType);
            double colWidth = columnWidth(columnType);
            double colHeight = columnHeight(columnType);

            if (top >= colTop && top <= (colTop + colHeight) && left >= colLeft && left <= (colLeft + colWidth)) return true;
            else return false;
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
                directions.Add(new SemanticResultValue("computer go back", "GO BACK"));
                directions.Add(new SemanticResultValue("lets go back", "GO BACK"));
                directions.Add(new SemanticResultValue("computer start", "START"));
                directions.Add(new SemanticResultValue("lets start", "START"));
                directions.Add(new SemanticResultValue("please start", "START"));
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
                    case "GO BACK":
                        this.window.Content = new WordSort(window, kinect);
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
