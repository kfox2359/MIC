using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
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
using WMPLib;

namespace Words_With_Kinect.Spelling_Game
{
    /// <summary>
    /// Interaction logic for SpellingGameScreen.xaml
    /// </summary>
    public partial class SpellingGameScreen : UserControl
    {
        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;

        /// <summary>
        /// Active Kinect sensor.
        /// </summary>
        private KinectSensor _sensor;

        private string word = "";

        private MainWindow _window;

        public SpellingGameScreen(MainWindow window, KinectSensor kinect)
        {
            InitializeComponent();
            _sensor = kinect;
            _window = window;
            Initialize();
        }

        private void Initialize()
        {
            RecognizerInfo ri = GetKinectRecognizer();

            if (null != ri)
            {

                this.speechEngine = new SpeechRecognitionEngine(ri.Id);

                var directions = new Choices();
                #region Load Letters
                directions.Add(new SemanticResultValue("a", "a"));
                directions.Add(new SemanticResultValue("b", "b"));
                directions.Add(new SemanticResultValue("c", "c"));
                directions.Add(new SemanticResultValue("d", "d"));
                directions.Add(new SemanticResultValue("e", "e"));
                directions.Add(new SemanticResultValue("f", "f"));
                directions.Add(new SemanticResultValue("g", "g"));
                directions.Add(new SemanticResultValue("h", "h"));
                directions.Add(new SemanticResultValue("i", "i"));
                directions.Add(new SemanticResultValue("j", "j"));
                directions.Add(new SemanticResultValue("k", "k"));
                directions.Add(new SemanticResultValue("l", "l"));
                directions.Add(new SemanticResultValue("m", "m"));
                directions.Add(new SemanticResultValue("n", "n"));
                directions.Add(new SemanticResultValue("o", "o"));
                directions.Add(new SemanticResultValue("p", "p"));
                directions.Add(new SemanticResultValue("q", "q"));
                directions.Add(new SemanticResultValue("r", "r"));
                directions.Add(new SemanticResultValue("s", "s"));
                directions.Add(new SemanticResultValue("t", "t"));
                directions.Add(new SemanticResultValue("u", "u"));
                directions.Add(new SemanticResultValue("v", "v"));
                directions.Add(new SemanticResultValue("w", "w"));
                directions.Add(new SemanticResultValue("x", "x"));
                directions.Add(new SemanticResultValue("y", "y"));
                directions.Add(new SemanticResultValue("z", "z"));
                directions.Add(new SemanticResultValue("z", "z"));
                directions.Add(new SemanticResultValue("eye", "i"));
                directions.Add(new SemanticResultValue("cay", "k"));
                directions.Add(new SemanticResultValue("see", "c"));
                directions.Add(new SemanticResultValue("sea", "c"));
                directions.Add(new SemanticResultValue("you", "u"));
                directions.Add(new SemanticResultValue("why", "y"));
                directions.Add(new SemanticResultValue("clear", "clear"));
                directions.Add(new SemanticResultValue("definition", "definition"));

                #endregion
                var gb = new GrammarBuilder { Culture = ri.Culture };
                gb.Append(directions);

                var g = new Grammar(gb);

                speechEngine.LoadGrammar(g);

                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                speechEngine.SetInputToAudioStream(
                _sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

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
        /// Handler for recognized speech events.
        /// </summary>              
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.65;
            //ClearRecognitionHighlights();
            //this.SpeechKinectGot.Text = youSaidText + e.Result.Semantics.Value.ToString();
            //this.SpeechRecConf.Text = speechRecConfText + e.Result.Confidence.ToString();
            if (e.Result.Confidence >= ConfidenceThreshold)
            //if (e.Result.Semantics.Value.ToString() != null && !e.Result.Semantics.Value.ToString().Equals(""))
            {
                confidence.Content = "Confidence Level: " + e.Result.Confidence;
                #region Words Rec
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "a":
                        word += "a ";
                        break;
                    case "b":
                        word += "b ";
                        break;
                    case "c":
                        word += "c ";
                        break;
                    case "d":
                        word += "d ";
                        break;
                    case "e":
                        word += "e ";
                        break;
                    case "f":
                        word += "f ";
                        break;
                    case "g":
                        word += "g ";
                        break;
                    case "h":
                        word += "h ";
                        break;
                    case "i":
                        word += "i ";
                        break;
                    case "j":
                        word += "j ";
                        break;
                    case "k":
                        word += "k ";
                        break;
                    case "l":
                        word += " l";
                        break;
                    case "m":
                        word += "m ";
                        break;
                    case "n":
                        word += "n ";
                        break;
                    case "o":
                        word += "o ";
                        break;
                    case "p":
                        word += "p ";
                        break;
                    case "q":
                        word += "q ";
                        break;
                    case "r":
                        word += "r ";
                        break;
                    case "s":
                        word += "s ";
                        break;
                    case "t":
                        word += "t ";
                        break;
                    case "u":
                        word += "u ";
                        break;
                    case "v":
                        word += "v ";
                        break;
                    case "w":
                        word += "w ";
                        break;
                    case "x":
                        word += "x ";
                        break;
                    case "y":
                        word += "y ";
                        break;
                    case "z":
                        word += "z ";
                        break;
                    case "clear":
                        word = "";
                        break;
                    case "definition":
                        /* try changing this to mp3 */
                        Letter.Content = "Reading def";
                        WindowsMediaPlayer player = new WindowsMediaPlayer();
                        byte[] b = Properties.Resources.cat_def;
                        FileInfo fileInfo = new FileInfo("cat_def.wma");
                        FileStream fs = fileInfo.OpenWrite();
                        fs.Write(b,0,b.Length);
                        fs.Close();
                        player.URL = fileInfo.Name;
                        player.controls.play();
                        break;
                        

                }
                #endregion
                Letter.Content = word;
            }
        }

        /// <summary>
        /// Handler for rejected speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
           // Letter.Content = e.Result.Semantics.Value.ToString();
        }
     
    }
}
