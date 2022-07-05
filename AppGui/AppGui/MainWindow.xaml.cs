using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using mmisharp;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using HtmlAgilityPack;
using System.Media;
using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AppGui
{
    public partial class MainWindow : Window
    {



        private MmiCommunication mmiC;

        private LifeCycleEvents lce;
        private MmiCommunication fusion, mmic;
        private IWebDriver webDriver;
        private String banco;

        public MainWindow()
        {
            InitializeComponent();
            //Creates the ChomeDriver object, Executes tests on Google Chrome
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;



            webDriver = new ChromeDriver(path + @"/driver/");
            webDriver.Navigate().GoToUrl("https://www.pokernow.club/start-game");

            mmiC = new MmiCommunication("localhost", 8000, "User1", "GUI");
            mmiC.Message += MmiC_Message;
            mmiC.Start();

            //init LifeCycleEvents..
            lce = new LifeCycleEvents("APP", "TTS", "User1", "na", "command"); // LifeCycleEvents(string source, string target, string id, string medium, string mode
            // MmiCommunication(string IMhost, int portIM, string UserOD, string thisModalityName)
            mmic = new MmiCommunication("localhost", 8000, "User1", "GUI");
        }

        private void MmiC_Message(object sender, MmiEventArgs e)
        {
            Console.WriteLine(e.Message);
            var doc = XDocument.Parse(e.Message);
            var com = doc.Descendants("command").FirstOrDefault().Value;
            dynamic json = JsonConvert.DeserializeObject(com);
            Console.WriteLine(json.recognized);
            /*RAISE("[action][RAISE]"),
            BET("[action][BET]"),
            FOLD("[action][FOLD]"),
            CHECK("[action][CHECK]"),
            RAISE_VALUE_MIN("[action][RAISE][value][MIN]"),
            RAISE_VALUE_DUP("[action][RAISE][value][DUPL]"),
            RAISE_VALUE_ALL("[action][RAISE][value][ALL]"),
            CHAT("[action][CHAT]"),
            PAUSE("[action][PAUSE]"),
            END("[action][END]"),
            START("[action][START]"),
            ;*/

            string[] repeat = { "Desculpe, não percebi, pode repetir?", "Não o consegui ouvir, pode repetir por favor?", "Poderia repetir se faz favor? Não percebi bem" };
            

            if (webDriver.FindElements(By.XPath("//div[@class='config-warning-popover']//button")).Count() > 0)
            {
                webDriver.FindElement(By.XPath("//div[@class='config-warning-popover']//button")).Click();
            }
            foreach (String c in json.recognized)
            {
                var command = c.Split(':')[0];
                switch (command)
                {
                        case "SIT":
                            String newbanco = ((String)c.Split(':')[1]).ToString();
                            String dinheiro = ((String)c.Split(':')[2]).ToString();
                            if (int.Parse(newbanco) > 10)
                            {
                                sendMessageToTts("Peço desculpa, esse banco não existe, escolha um entre 1 e 10");
                            }
                            else
                            {

                                try
                                {
                                    webDriver.FindElement(By.XPath("//div[@class='table-player table-player-seat table-player-" + newbanco + "']/button")).Click();

                                    webDriver.FindElement(By.XPath("//input[@placeholder='Your Stack']")).SendKeys(dinheiro);
                                    IWebElement buttonText = webDriver.FindElement(By.XPath("//button[@class='button-1 med-button highlighted green']"));
                                    Console.WriteLine(buttonText.GetAttribute("innerHTML"));
                                    buttonText.Click();
                                    banco = newbanco;
                                    sendMessageToTts("Sentado no banco " + newbanco + " com " + dinheiro + " euros");
                                }
                                catch
                                {
                                    sendMessageToTts("Já estás sentado no banco " + banco);
                                }
                            }
                            break;
                        case "BOTH": //  both arms up
                        try
                        {
                            if (webDriver.FindElements(By.XPath("//div[@class='top-buttons ']//button[@class='top-buttons-button options ']")).Count() > 0)
                            {
                                webDriver.FindElement(By.XPath("//div[@class='top-buttons ']//button[@class='top-buttons-button options ']")).Click();
                            }
                        }
                        catch { }
                      
                        break;
                    case "PLAYERS": //  both arms up
                 
                        try
                        {
                            IList<IWebElement> webElements = webDriver.FindElements(By.XPath("//div[@class='config-top-tabs']//button"));
                            webElements[1].Click();
                            break;
                        }
                        catch { }
                        break;
                    case "OPTIONS": //  both arms up

                        try
                        {
                            IList<IWebElement> webElements = webDriver.FindElements(By.XPath("//div[@class='config-top-tabs']//button"));
                            webElements[2].Click();
                            break;
                        }
                        catch { }
                        break;
                    case "RAISE":
                        try
                        {
                            webDriver.FindElement(By.XPath("//div[@class='action-buttons game-decisions']//button[@class='button-1 with-tip raise green']")).Click();
                        }
                        catch { }
                        
                        break;
                    case "FOLD": // FOLD - arm cross and de-cross
                        try
                        {
                            try
                            {
                                webDriver.FindElement(By.XPath("//div[@class='action-buttons game-decisions']//button[@class='button-1 with-tip fold red ']")).Click();
                            }
                            catch { }
                        }
                        catch { }
                        break;
                    case "CHECK": // CHECK - horizontal arm swipe
                        try
                        {
                            try
                            {
                                webDriver.FindElement(By.XPath("//div[@class='action-buttons game-decisions']//button[@class='button-1 with-tip check green ']")).Click();

                            }
                            catch { }
                        }
                        catch { }
                        break;
                    case "BET": // BET - thumbs up
                        try
                        {
                            webDriver.FindElement(By.XPath("//div[@class='action-buttons game-decisions']//button[@class='button-1 call with-tip call green ']")).Click();
                        }
                        catch { }
                      
                        break;
                    case "MIN":
                        try
                        {
                            IList<IWebElement> webElements = webDriver.FindElements(By.XPath("//div[@class='default-bet-buttons']//button"));
                            webElements[0].Click();
                            webDriver.FindElement(By.XPath("//div[@class='action-buttons']//input[@class='button-1 green bet']")).Click();
                        }
                        catch { }

                        break;
                    case "DUPL":
                        try
                        {
                            IList<IWebElement> webElements = webDriver.FindElements(By.XPath("//div[@class='default-bet-buttons']//button"));
                            webElements[3].Click();
                            webDriver.FindElement(By.XPath("//div[@class='action-buttons']//input[@class='button-1 green bet']")).Click();
                        }
                        catch { }

                        break;
                    case "ALL":
                        try
                        {
                            IList<IWebElement> webElements = webDriver.FindElements(By.XPath("//div[@class='default-bet-buttons']//button"));
                            webElements[4].Click();
                            webDriver.FindElement(By.XPath("//div[@class='action-buttons']//input[@class='button-1 green bet']")).Click();
                        }
                        catch { }
                        break;
                    case "CHATON":
                        if (webDriver.FindElements(By.XPath("//div[@class='conf-controls']//button")).Count() > 0)
                        {

                            var turnedOn = 0;
                            foreach (IWebElement elem in webDriver.FindElements(By.XPath("//div[@class='conf-controls']//button")))
                                {
                                    if (!elem.GetAttribute("class").Contains("active"))
                                    {
                                        elem.Click();
                                        turnedOn++;
                                }
                                }
                            if (turnedOn > 0)
                                sendMessageToTts("Atenção, a câmara está a ligar!");

                            sendMessageToTts("A ligar a video chamada ");
                        }
                        break;
                    case "CHATOFF":
                        if (webDriver.FindElements(By.XPath("//div[@class='conf-controls']//button")).Count() > 0)
                        {
                            
                                foreach (IWebElement elem in webDriver.FindElements(By.XPath("//div[@class='conf-controls']//button")))
                                {
                                    if (elem.GetAttribute("class").Contains("active"))
                                    {
                                        elem.Click();
                                    }
                                }
                             
                            
                        }
                        break;
                    case "PAUSE":
                        if (webDriver.FindElements(By.XPath("//button[@class='button-1 dark-gray small-button pause-game-button not-paused']")).Count() > 0)
                        {
                            webDriver.FindElement(By.XPath("//button[@class='button-1 dark-gray small-button pause-game-button not-paused']")).Click();
                            sendMessageToTts("O jogo está em pausa");
                        }
                            
                        break;
                    case "RESUME":
                        if (webDriver.FindElements(By.XPath("//button[@class='button-1 dark-gray small-button pause-game-button paused']")).Count() > 0)
                        {
                            webDriver.FindElement(By.XPath("//button[@class='button-1 dark-gray small-button pause-game-button paused']")).Click();
                            sendMessageToTts("O jogo foi resumido");
                        }
                            
                        break;
                    case "END":
                        if (webDriver.FindElements(By.XPath("//button[@class='button-1 dark-gray small-button stop-game-button ']")).Count() > 0)
                        {
                            webDriver.FindElement(By.XPath("//button[@class='button-1 dark-gray small-button stop-game-button ']")).Click();
                            sendMessageToTts("Este será o último jogo");
                        }
                            
                        break;

                    case "START":
                        try
                        {
                            try
                            {
                                webDriver.FindElement(By.XPath("//div[@class='out-game-decisions action-buttons']//button[@class='button-1 green highlighted']")).Click();
                            }
                            catch { }
                        }
                        catch { }
                        break;
                    default:
                        break;
                }
            
            }
        }

        public void sendMessageToTts(String s)
        {
            mmic.Send(lce.NewContextRequest());

            string json2 = "";
            json2 += s;
            var exNot = lce.ExtensionNotification(0 + "", 0 + "", 1, json2);
            mmic.Send(exNot);
        }
    }

}
