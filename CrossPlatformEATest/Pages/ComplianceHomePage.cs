using EAAutoFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformEATest.Pages
{
 
    internal class ComplianceHomePage : BasePage
{
    public ComplianceHomePage(ParallelConfig parellelConfig) : base(parellelConfig) { }
    

        IWebElement txtName => _parallelConfig.Driver.FindElement(By.Id("Name"));

        IWebElement txtSalary => _parallelConfig.Driver.FindElement(By.Id("Salary"));

        IWebElement txtDurationWorked => _parallelConfig.Driver.FindElement(By.Id("DurationWorked"));

        IWebElement txtGrade => _parallelConfig.Driver.FindElement(By.Id("Grade"));

        IWebElement txtEmail => _parallelConfig.Driver.FindElement(By.Id("Email"));

        IWebElement btnClickUpload => _parallelConfig.Driver.FindElement(
            By.CssSelector(
                "body > app-root > mat-sidenav-container > mat-sidenav > ul:nth-child(2) > li:nth-child(1)"));


    internal ComplianceHomePage ClickUploadButton()
    {
        btnClickUpload.Click();
        return new ComplianceHomePage(_parallelConfig);
    }


}
}

