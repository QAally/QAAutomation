using EAAutoFramework.Config;
using EAAutoFramework.Helpers;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.IO;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Remote;
using System;
using OpenQA.Selenium;

namespace EAAutoFramework.Base
{
    public class TestInitializeHook : Steps
    {

        private readonly ParallelConfig _parallelConfig;

        public TestInitializeHook(ParallelConfig parallelConfig)
        {
            _parallelConfig = parallelConfig;
        }


        public void InitializeSettings()
        {
            //Set all the settings for framework
            ConfigReader.SetFrameworkSettings();

            //Set Log
            LogHelpers.CreateLogFile();

            //Open Browser
            OpenBrowser(Settings.BrowserType);

            LogHelpers.Write("Initialized framework");

        }

        private void OpenBrowser(BrowserType browserType) // = BrowserType.Chrome)
        {
            DesiredCapabilities cap = new DesiredCapabilities();
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    cap.SetCapability(CapabilityType.BrowserName, "IE");
                    //ToDo: Set the Desired capabilities
                    _parallelConfig.Driver = new InternetExplorerDriver();
                    _parallelConfig.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                    break;
                case BrowserType.FireFox:
                    cap.SetCapability(CapabilityType.BrowserName, "firefox");
                    cap.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                    //var binary = new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    var profile = new FirefoxProfile();
                     FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("C:\\webdriverss\\");
                    _parallelConfig.Driver = new FirefoxDriver(service);
                    _parallelConfig.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

                    break;
                case BrowserType.Chrome:
                     cap.SetCapability(CapabilityType.BrowserName, "chrome");
                    _parallelConfig.Driver = new ChromeDriver();
                    _parallelConfig.Driver.Manage().Window.Maximize();
                    _parallelConfig.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                    break;
            }



            //_parallelConfig.Driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), cap);
            //_parallelConfig.Driver = new RemoteWebDriver(new Uri("http://remoteServer:4444/wd/hub"), cap);
        }

        public virtual void NaviateSite()
        {
            //DriverContext.Browser.GoToUrl(Settings.AUT);
            LogHelpers.Write("Opened the browser !!!");
        }



    }
}
