using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using TSendKeys = System.Windows.Forms.SendKeys;

namespace Emma
{
    public partial class Emma : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer synth = new SpeechSynthesizer();
        SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
        SpeechRecognitionEngine dictModeSleep = new SpeechRecognitionEngine();
        SpeechRecognitionEngine dictMode = new SpeechRecognitionEngine();
        SpeechRecognitionEngine spelling = new SpeechRecognitionEngine();
        Random rnd = new Random();
        int RecTimeOut = 0;
        DateTime Time = DateTime.Now;

        public string name = "Emma";
        public string username;


        public Emma()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
            _recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

            startlistening.SetInputToDefaultAudioDevice();
            startlistening.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"ListeningCommands.txt")))));
            startlistening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startlistening_SpeechRecognized);

            

            this.Location = Screen.AllScreens[1].WorkingArea.Location;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            usernameBox1.Visible = false;
            OKbtn1.Visible = false;

            //get nameFile

            if (!File.Exists("usernameFile.txt"))
            {
                usernameBox1.Visible = true;
                OKbtn1.Visible = true;
                synth.SpeakAsync("Please enter your name. When you're done press OK");
                
            }

            else
            {
                using (StreamReader nameStreamReader = new StreamReader("usernameFile.txt")) 
                {
                    username = nameStreamReader.ReadLine();
                }
            }

            synth.SpeakAsync("Hello " + username + ". How can I help you?");
        }

        

        private void Default_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int ranNum;
            string speech = e.Result.Text;

            if (speech == "What's your name")
            {
                synth.SpeakAsync("I'm " + name);
            }
            else if (speech == "What's my name")
            {
                synth.SpeakAsync("You are " + username);
            }
            else if (speech == "Change my name")
            {
                File.Delete("usernameFile.txt");
                usernameBox1.Visible = true;
                OKbtn1.Visible = true;
                synth.SpeakAsync("Please enter your name. When you're done press OK");
            }
            else if (speech == "How are you")
            {
                synth.SpeakAsync("I'm working properly");
            }
            else if (speech == "What time is it")
            {
                synth.SpeakAsync(DateTime.Now.ToString("h mm ss"));
            }
            else if (speech == "Stop talking")
            {
                synth.SpeakAsyncCancelAll();
                ranNum = rnd.Next(1);
                if (ranNum == 1)
                {
                    synth.SpeakAsync("Yes sir");
                }
                else if (ranNum == 2)
                {
                    synth.SpeakAsync("Yes sir, I will be quiet");
                }
            }
            else if (speech == "Stop listening")
            {
                synth.SpeakAsync("If you need me just ask");
                _recognizer.RecognizeAsyncCancel();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
            }
            else if (speech == "Show commands")
            {
                string[] Listeningcommands = (File.ReadAllLines(@"ListeningCommands.txt"));
                foreach (string Listeningcommand in Listeningcommands)
                {
                    LstCommands.Items.Add(Listeningcommand);
                }
                string[] Defaultcommands = (File.ReadAllLines(@"DefaultCommands.txt"));
                LstCommands.Items.Clear();
                LstCommands.SelectionMode = SelectionMode.None;
                LstCommands.Visible = true;
                LstCommands.BackColor = SystemColors.Control;
                LstCommands.Items.Add("\n \n");
                foreach (string Defaultcommand in Defaultcommands)
                {
                    LstCommands.Items.Add(Defaultcommand);
                }

            }
            else if (speech == "Hide commands")
            {
                LstCommands.Visible = false;
            }
            else if (speech == "Close program")
            {
                System.Windows.Forms.Application.Exit();
            }

            //Enter dictation mode
            else if (speech == "Enter dictation mode")
            {
                synth.SpeakAsync("Please wait, while I load my grammar.");
                dictMode.SetInputToDefaultAudioDevice();
                dictMode.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DictCommands.txt")))));
                dictMode.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"AllWords.txt")))));
                dictMode.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(dictMode_SpeechRecognized);

                dictModeSleep.SetInputToDefaultAudioDevice();
                dictModeSleep.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"ListeningCommands.txt")))));
                dictModeSleep.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(dictModeSleep_SpeechRecognized);

                spelling.SetInputToDefaultAudioDevice();
                spelling.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"spellingCommands.txt")))));
                spelling.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"AllLetters.txt")))));
                spelling.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(spelling_SpeechRecognized);

                synth.SpeakAsync("I'm now in dictation mode");
                _recognizer.RecognizeAsyncCancel();
                dictMode.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

       

        private void YesNo_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string YesNoSpeech = e.Result.Text;
        }

        private void _recognizer_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        {
            RecTimeOut = 0;
        }

        private void startlistening_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "Wake up" || speech == "Hi " + name)
            {
                startlistening.RecognizeAsyncCancel();
                synth.SpeakAsync("Yes,I'm here");
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void dictMode_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "Quit dictation mode")
            {
                dictMode.RecognizeAsyncCancel();
                synth.SpeakAsync("I'm now in normal mode");
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }

            else if (speech == "Stop listening")
            {
                synth.SpeakAsync("If you need me just ask");
                dictMode.RecognizeAsyncCancel();
                dictModeSleep.RecognizeAsync(RecognizeMode.Multiple);
            }

            else if (speech == "spelling mode")
            {
                synth.SpeakAsync("I'm now in spelling mode");
                dictMode.RecognizeAsyncCancel();
                spelling.RecognizeAsync(RecognizeMode.Multiple);
            }

            else if (speech == "new line" || speech == "Enter")
            {
                TSendKeys.Send("{ENTER}");
            }
            else if (speech == "dot")
            {
                TSendKeys.Send(".");
            }
            else if (speech == "question mark")
            {
                TSendKeys.Send("?");
            }
            else if (speech == "space")
            {
                TSendKeys.Send(" ");
            }
            else if (speech == "comma")
            {
                TSendKeys.Send(",");
            }

            else if (speech == "Show commands")
            {
                string[] Listeningcommands = (File.ReadAllLines(@"ListeningCommands.txt"));
                foreach (string Listeningcommand in Listeningcommands)
                {
                    LstCommands.Items.Add(Listeningcommand);
                }
                string[] dictCommands = (File.ReadAllLines(@"DictCommands.txt"));
                LstCommands.Items.Clear();
                LstCommands.SelectionMode = SelectionMode.None;
                LstCommands.Visible = true;
                LstCommands.BackColor = SystemColors.Control;
                LstCommands.Items.Add("\n \n");
                foreach (string dictCommand in dictCommands)
                {
                    LstCommands.Items.Add(dictCommand);
                }
            }
            else if (speech == "Hide commands")
            {
                LstCommands.Visible = false;
            }


            else if (speech == "Close program")
            {
                System.Windows.Forms.Application.Exit();
            }

            else
            {
                TSendKeys.Send(speech);
            }
        }

        private void dictModeSleep_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "Wake up" || speech == "Hi Emma")
            {
                startlistening.RecognizeAsyncCancel();
                synth.SpeakAsync("I'm in dictation mode");
                dictMode.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void spelling_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            if (speech == "Quit spelling mode")
            {
                synth.SpeakAsync("I'm now in dictation mode");
                spelling.RecognizeAsyncCancel();
                dictMode.RecognizeAsync(RecognizeMode.Multiple);
            }

            else if (speech == "new line" || speech == "Enter")
            {
                TSendKeys.Send("{ENTER}");
            }
            else if (speech == "backspace")
            {
                TSendKeys.Send("{BACKSPACE}");
            }
            else if (speech == "dot")
            {
                TSendKeys.Send(".");
            }
            else if (speech == "question mark")
            {
                TSendKeys.Send("?");
            }
            else if (speech == "space")
            {
                TSendKeys.Send(" ");
            }
            else if (speech == "comma")
            {
                TSendKeys.Send(",");
            }

            else
            {
                TSendKeys.Send(speech);
            }
        }


        private void TmrSpeaking_Tick(object sender, EventArgs e)
        {
            if (RecTimeOut == 10)
            {
                _recognizer.RecognizeAsyncCancel();
            }
            else if (RecTimeOut == 11)
            {
                TmrSpeaking.Stop();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
                RecTimeOut = 0;
            }
        }

        private void Emma_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void OKbtn1_Click(object sender, EventArgs e)
        {
            username = usernameBox1.Text;
            File.Create("nameFile.txt").Close();
            using (StreamWriter nameStreamWriter = File.AppendText("usernameFile.txt"))
            {
                nameStreamWriter.WriteLine(username);
            }
            using (StreamReader nameStreamReader = new StreamReader("usernameFile.txt"))
            {
                username = nameStreamReader.ReadLine();
            }

            usernameBox1.Visible = false;
            OKbtn1.Visible = false;
        }
    }
}
