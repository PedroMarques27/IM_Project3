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
    public partial class MainWindow: Window
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


            if (webDriver.FindElements(By.XPath("//div[@class='config-warning-popover']//button")).Count() > 0)
            {
                webDriver.FindElement(By.XPath("//div[@class='config-warning-popover']//button")).Click();
            }
            foreach (String command in json.recognized)
            {
                switch (command)
                {
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
                    case "PLAYERS": //  both arms up

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
                            try
                            {
                                string current = webDriver.FindElement(By.XPath("//div[@class='raise-bet-value ']//div[@class='value-input-ctn']//input")).GetAttribute("value");
                                string money = webDriver.FindElement(By.XPath("//p[@class='blind-value']//span[@class='normal-value']")).GetAttribute("innerHTML");
                                int value = int.Parse(current) + int.Parse(money);

                                IWebElement element = webDriver.FindElement(By.XPath("//div[@class='value-input-ctn']//input"));
                                element.Click();
                                element.Clear();
                                element.SendKeys(value.ToString());
                            }
                            catch { }
                        }
                        catch { }
                        break;
                    case "FOLD": // FOLD - stomp leg
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
                            try
                            {
                                webDriver.FindElement(By.XPath("//div[@class='action-buttons game-decisions']//button[@class='button-1 call with-tip call green ']")).Click();
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

    }
}
