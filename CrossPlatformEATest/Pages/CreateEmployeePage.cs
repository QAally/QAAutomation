using EAAutoFramework.Base;
using OpenQA.Selenium;

namespace CrossPlatformEATest.Pages
{
    internal class CreateEmployeePage : BasePage
    {
        public CreateEmployeePage(ParallelConfig parellelConfig) : base(parellelConfig)
        {
        }

        IWebElement txtName => _parallelConfig.Driver.FindElement(By.Id("Name"));

        IWebElement txtSalary => _parallelConfig.Driver.FindElement(By.Id("Salary"));

        IWebElement txtDurationWorked => _parallelConfig.Driver.FindElement(By.Id("DurationWorked"));

        IWebElement txtGrade => _parallelConfig.Driver.FindElement(By.Id("Grade"));

        IWebElement txtEmail => _parallelConfig.Driver.FindElement(By.Id("Email"));

        IWebElement btnCreateEmployee => _parallelConfig.Driver.FindElement(By.XPath("//input[@value='Create']"));


        internal EmployeeListPage ClickCreateButton()
        {
            btnCreateEmployee.Submit();
            return new EmployeeListPage(_parallelConfig);
        }


        internal void CreateEmployee(string name, string salary, string durationworked, string grade, string email)
        {
            txtName.SendKeys(name);
            txtSalary.SendKeys(salary);
            txtDurationWorked.SendKeys(durationworked);
            txtGrade.SendKeys(grade);
            txtEmail.SendKeys(email);
        }

    }
}
