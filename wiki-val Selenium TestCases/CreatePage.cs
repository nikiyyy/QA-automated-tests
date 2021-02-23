using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace wiki_val_QA_tests
{
    public class Createpage_Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://wiki-val.herokuapp.com/new_page/");
        }

        [Test]
        public void Test_Load_CreateNewPage()
        {
            Assert.AreEqual("Create New Page", driver.Title);
            Assert.AreEqual(driver.FindElement(By.CssSelector("p:nth-child(3)")).Text, "Title:");
            Assert.AreEqual(driver.FindElement(By.CssSelector("p:nth-child(4)")).Text, "Content:");
            Assert.IsTrue(driver.FindElement(By.Id("id_title")).Displayed);
            Assert.IsTrue(driver.FindElement(By.Id("id_content")).Displayed);
            Assert.IsTrue(driver.FindElement(By.Id("button")).Displayed);
        }
        [Test]
        public void Test_CreateNewPage()
        {
            Random rand = new Random();
            int concInt = rand.Next(10000);
            driver.FindElement(By.Id("id_title")).Click();
            driver.FindElement(By.Id("id_title")).SendKeys("TestPage"+ concInt);
            driver.FindElement(By.Id("id_content")).Click();
            driver.FindElement(By.Id("id_content")).SendKeys("TestPage" + concInt);
            driver.FindElement(By.Id("button")).Click();

            Assert.AreEqual(driver.Title, "TestPage" + concInt);

        }
        [Test]
        public void Test_CreateNewPage_invalidTitle()
        {
            driver.FindElement(By.Id("id_title")).Click();
            driver.FindElement(By.Id("id_title")).SendKeys("willNotWork");

            Assert.AreEqual("Create New Page", driver.Title);
        }
        [Test]
        public void Test_CreateNewPage_invalidContent()
        {
            driver.FindElement(By.Id("id_content")).Click();
            driver.FindElement(By.Id("id_content")).SendKeys("willNotWork");

            Assert.AreEqual("Create New Page", driver.Title);

        }

        [Test]
        public void Test_CreateNewPage_existingPage()
        {
            driver.FindElement(By.Id("id_title")).Click();
            driver.FindElement(By.Id("id_title")).SendKeys("Python");
            driver.FindElement(By.Id("id_content")).Click();
            driver.FindElement(By.Id("id_content")).SendKeys("willNotWork");
            driver.FindElement(By.Id("button")).Click();

            Assert.AreEqual("A page with that name already exists!", driver.FindElement(By.CssSelector("h3")).Text);

        }

        [Test]
        public void Test_Editpage()
        {
            Random rand = new Random();
            int concInt = rand.Next(10000);
            driver.FindElement(By.Id("id_title")).Click();
            driver.FindElement(By.Id("id_title")).SendKeys("TestPage" + concInt);
            driver.FindElement(By.Id("id_content")).Click();
            driver.FindElement(By.Id("id_content")).SendKeys("TestPage" + concInt);
            driver.FindElement(By.Id("button")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("edit-link")).Click();
            driver.FindElement(By.Id("id_content")).Clear();
            Thread.Sleep(500);
            driver.FindElement(By.Id("id_content")).SendKeys("edited");
            //button 
            driver.FindElement(By.Id("button")).Click();
            Assert.AreEqual("edited", driver.FindElement(By.CssSelector("p:nth-child(2)")).Text);
        }

        [TearDown]

        protected void TearDown()
        {
            driver.Quit();
        }
    }
}