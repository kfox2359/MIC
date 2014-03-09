using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words_With_Kinect.Spelling_Game
{
    public class SpeechRec
    {
        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;

        /// <summary>
        /// Active Kinect sensor.
        /// </summary>
        private KinectSensor _sensor;

        public SpeechRec(KinectSensor kinect)
        {
            _sensor = kinect;
            Initialize();
        }

        private void Initialize()
        {
            RecognizerInfo ri = GetKinectRecognizer();

            if (null != ri)
            {

                this.speechEngine = new SpeechRecognitionEngine(ri.Id);



                
                // Create a grammar from grammar definition XML file.
                using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.SpeechGrammer)))
                {
                    var g = new Grammar(memoryStream);
                    speechEngine.LoadGrammar(g);
                }
               
                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                ////speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

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
            const double ConfidenceThreshold = 0.7;
            string temp = "";
            //ClearRecognitionHighlights();
            //this.SpeechKinectGot.Text = youSaidText + e.Result.Semantics.Value.ToString();
            //this.SpeechRecConf.Text = speechRecConfText + e.Result.Confidence.ToString();
            if (e.Result.Confidence >= ConfidenceThreshold)
            //if (e.Result.Semantics.Value.ToString() != null && !e.Result.Semantics.Value.ToString().Equals(""))
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "a":
                        temp = "a";
                       // this.FeedbackText.Text = KinectSaysText + "Hello there.";
                        break;

                    case "b":
                        temp = "b";
                       // this.FeedbackText.Text = KinectSaysText + "Bye, see you soon.";
                        break;
                    case "c":
                        temp="c";
                        //this.FeedbackText.Text = "Reseted";
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
