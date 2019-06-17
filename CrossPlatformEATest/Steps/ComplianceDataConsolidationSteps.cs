using System;
using CrossPlatformEATest.Pages;
using EAAutoFramework.Base;
using TechTalk.SpecFlow;

namespace CrossPlatformEATest.Steps
{
    [Binding]
    class ComplianceDataConsolidationSteps: BaseStep
    { 
    //Context injection
    private readonly ParallelConfig _parallelConfig;

    public ComplianceDataConsolidationSteps(ParallelConfig parellelConfig) : base(parellelConfig)
    {
    _parallelConfig = parellelConfig;
    }

        [Given(@"I click on the uploads button")]
        public void GivenIClickOnTheUploadsButton()
        {
            _parallelConfig.CurrentPage.As<ComplianceHomePage>().ClickUploadButton();

        }

        [Given(@"I choose to upload a valid Action update Assessment file")]
        public void GivenIChooseToUploadAValidActionUpdateAssessmentFile()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
