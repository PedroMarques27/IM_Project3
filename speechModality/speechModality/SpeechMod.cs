﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmisharp;
using Microsoft.Speech.Recognition;
using System.Xml.Linq;
//using Newtonsoft.Json;

namespace speechModality
{
    public class SpeechMod
    {
        // changed 16 april 2020
        private static SpeechRecognitionEngine sre= new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-PT"));
        private Grammar gr;


        public event EventHandler<SpeechEventArg> Recognized;
        protected virtual void onRecognized(SpeechEventArg msg)
        {
            EventHandler<SpeechEventArg> handler = Recognized;
            if (handler != null)
            {
                handler(this, msg);
            }
        }

        private LifeCycleEvents lce;
        private MmiCommunication mmic;

        //  NEW 16 april
        private static Tts tts = new Tts(sre);
        private MmiCommunication mmiReceiver;

        public SpeechMod()
        {
            //init LifeCycleEvents..
            lce = new LifeCycleEvents("ASR", "FUSION", "speech-1", "acoustic", "command"); // LifeCycleEvents(string source, string target, string id, string medium, string mode)
            mmic = new MmiCommunication("localhost",9876,"User1", "ASR");  //PORT TO FUSION - uncomment this line to work with fusion later

            mmic.Send(lce.NewContextRequest());

            //load pt recognizer
            //sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-PT"));
            gr = new Grammar(Environment.CurrentDirectory + "\\ptG.grxml", "rootRule");
            sre.LoadGrammar(gr);

            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);
            sre.SpeechRecognized += Sre_SpeechRecognized;
            sre.SpeechHypothesized += Sre_SpeechHypothesized;

            // NEW - TTS support 16 April
            tts.Speak("Olá, sou a Ema. Estou pronta para receber ordens.");

            //  o TTS  no final indica que se recebe mensagens enviadas para TTS
            mmiReceiver = new MmiCommunication("localhost",8000, "User1", "TTS");
            mmiReceiver.Message += MmiReceived_Message;
            mmiReceiver.Start();
        }

    private void Sre_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            onRecognized(new SpeechEventArg() { Text = e.Result.Text, Confidence = e.Result.Confidence, Final = false });
        }

        //
        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence < 0.4)
            {
                
            }
            else if (e.Result.Confidence < 0.7)
            {

                tts.Speak("Peço desculpa, não percebi bem. Pode repetir?");
            }
            else 
            {
                onRecognized(new SpeechEventArg() { Text = e.Result.Text, Confidence = e.Result.Confidence, Final = true });

                //SEND
                // IMPORTANT TO KEEP THE FORMAT {"recognized":["SHAPE","COLOR"]}
                string json = "{ \"recognized\": [";
                foreach (var resultSemantic in e.Result.Semantics)
                {
                    json += "\"" + "action" + "\",\"" + resultSemantic.Value.Value + "\", ";
                }
                json = json.Substring(0, json.Length - 2);
                json += "] }";

                var exNot = lce.ExtensionNotification(e.Result.Audio.StartTime + "", e.Result.Audio.StartTime.Add(e.Result.Audio.Duration) + "", e.Result.Confidence, json);
                mmic.Send(exNot);
            }
          
        }

        //MmiReceived_Message;
        private void MmiReceived_Message(object sender, MmiEventArgs e)
        {
            Console.WriteLine(e.Message);

            var doc = XDocument.Parse(e.Message);
            var com = doc.Descendants("command").FirstOrDefault().Value;

            Console.WriteLine(com);

            tts.Speak(com);
        }
    }
}
