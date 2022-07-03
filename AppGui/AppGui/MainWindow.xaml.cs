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

            //Creates the ChomeDriver object, Executes tests on Google Chrome
            webDriver = new ChromeDriver(path + @"/driver/");



            mmiC = new MmiCommunication("localhost", 8000, "User1", "GUI");
            mmiC.Message += MmiC_Message;
            mmiC.Start();


            fusion = new MmiCommunication("localhost", 9876, "User1", "GUI");
            fusion.Message += MmiC_Message;
            fusion.Start();



        }

        private void MmiC_Message(object sender, MmiEventArgs e)
        {
            Console.WriteLine(e.Message);
            var doc = XDocument.Parse(e.Message);
            var com = doc.Descendants("command").LastOrDefault().Value;
            dynamic json = JsonConvert.DeserializeObject(com);
            Console.WriteLine(com.ToString());


        }

    }
}
