using System;
using OpenQA.Selenium;

namespace NUnitPOM.PageObjects
{
    public class BasePage
    {
        public IWebDriver Driver;
        public virtual string PageUrl { get; }
        public BasePage(IWebDriver Driver) {
            this.Driver = Driver;
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public IWebElement LinkHomePage => Driver.FindElement(By.XPath("//a[contains(., 'Home')]"));
        public IWebElement LinkStudents => Driver.FindElement(By.XPath("//a[contains(., 'View Students')]"));
        public IWebElement LinkAddStudents => Driver.FindElement(By.XPath("//a[contains(., 'Add Student')]"));
        public IWebElement PageHeading => Driver.FindElement(By.CssSelector("body > h1"));

        public void Open() {
            Driver.Navigate().GoToUrl(this.PageUrl);
        }
        public bool IsOpen() {
            return Driver.Url == this.PageUrl;
        }
        public string GetPageTitle() {
            return Driver.Title;
        }
        public string GetPageHeadingText() {
            return PageHeading.Text;
        }
    }

}
