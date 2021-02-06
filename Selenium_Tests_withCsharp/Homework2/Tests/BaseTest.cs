using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NUnitPOM.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}
