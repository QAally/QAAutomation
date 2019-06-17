using EAAutoFramework.Base;
using TechTalk.SpecFlow;
using EAAutoFramework.Helpers;
using EAAutoFramework.Config;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace CrossPlatformEATest.Hooks
{

    [Binding]
    public class HookInitialize : TestInitializeHook
    {
        private readonly ParallelConfig _parallelConfig;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;


        public HookInitialize(ParallelConfig parallelConfig, FeatureContext featureContext, ScenarioContext scenarioContext) : base(parallelConfig)
        {
            _parallelConfig = parallelConfig;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            
        }



        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static KlovReporter klov;


        [AfterStep]
        public void AfterEachStep()
        {
            var stepName = _scenarioContext.StepContext.StepInfo.Text;
            var featureName = _featureContext.FeatureInfo.Title;
            var scenarioName = _scenarioContext.ScenarioInfo.Title;

            //if (ScenarioContext.Current.TestError != null)
            //    _client.WriteTestResult(featureName, scenarioName, stepName, ScenarioContext.Current.TestError.StackTrace, "FAILED");
            //else
            //    _client.WriteTestResult(featureName, scenarioName, stepName, "", "PASSED");

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
            }
        }

        [BeforeTestRun]
        public static void TestInitalize()
        {
            //InitializeSettings();
            //Settings.ApplicationCon = Settings.ApplicationCon.DBConnect(Settings.AppConnectionString);

            //Initialize Extent report before test starts
            var htmlReporter = new ExtentHtmlReporter(@"C:\Development\EATestProject\EATestProject\ExtentReport.html");
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //Attach report to reporter
            extent = new ExtentReports();
            klov = new KlovReporter();

            //klov.InitMongoDbConnection("localhost", 27017);

            //klov.ProjectName = "ExecuteAutomation Test";

            //// URL of the KLOV server
            //klov.KlovUrl = "http://localhost:5689";

            //klov.ReportName = "Karthik KK" + DateTime.Now.ToString();


            extent.AttachReporter(htmlReporter);
        }


        [BeforeScenario]
        public void Initialize()
        {
            InitializeSettings();
            Settings.ApplicationCon = Settings.ApplicationCon.DBConnect(Settings.AppConnectionString);

            //Get feature Name
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);

            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }



        [AfterScenario]
        public void TestStop()
        {
            //DriverContext.Driver.Quit();
            //Flush report once test completes
            _parallelConfig.Driver.Quit();
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extent.Flush();

        }
        [OneTimeTearDown]
        public void tearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)_parallelConfig.Driver).GetScreenshot();
                screenshot.SaveAsFile(@"C:\\Development\\EATestProject\\Screenshots\\Screenshot.jpg");
            }

            extent.Flush();
            _parallelConfig.Driver.Close();
        }




    }
}
