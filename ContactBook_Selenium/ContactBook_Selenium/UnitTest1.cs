using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace ContactBook_Selenium
{
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:8080/");
        }

        [Test]
        public void assertFirstContact()
        {
            driver.FindElement(By.CssSelector(".home-page-icons > a:nth-child(1)")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#contact1 .fname > td")).Click();
            String contName = driver.FindElement(By.CssSelector(".fname > td")).Text + " " + driver.FindElement(By.CssSelector(".lname > td")).Text;
            //Thread.Sleep(1000);
            Assert.AreEqual("Steve Jobs", contName);
        }

        [Test]
        public void findContactByKeyword()
        {
            driver.FindElement(By.CssSelector(".home-page-icons > a:nth-child(3)")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("keyword")).Click();
            driver.FindElement(By.Id("keyword")).SendKeys("albert");
            driver.FindElement(By.Id("search")).Click();
            //driver.FindElement(By.CssSelector(".fname > td")).Click();
            String contName = driver.FindElement(By.CssSelector(".fname > td")).Text + " " + driver.FindElement(By.CssSelector(".lname > td")).Text;
            //Thread.Sleep(1000);
            Assert.AreEqual("Albert Einstein", contName);
        }


        [Test]
        public void findContactByKeyword_invalid()
        {
            driver.FindElement(By.CssSelector(".home-page-icons > a:nth-child(3)")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("keyword")).Click();
            driver.FindElement(By.Id("keyword")).SendKeys("Idonotexist");
            driver.FindElement(By.Id("search")).Click();
            //Thread.Sleep(1000);
            Assert.AreEqual("No contacts found.", driver.FindElement(By.CssSelector("#searchResult")).Text);
        }

        [Test]
        public void CreateNewContact()
        {//create
            driver.FindElement(By.CssSelector(".home-page-icons > a:nth-child(2)")).Click();
            Thread.Sleep(1000);

            Random rnd = new Random();
            int lname = rnd.Next(10000);
            driver.FindElement(By.Id("firstName")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("petkan");
            driver.FindElement(By.Id("lastName")).Click();
            driver.FindElement(By.Id("lastName")).SendKeys("draganov" + lname);
            driver.FindElement(By.Id("email")).Click();
            driver.FindElement(By.Id("email")).SendKeys("petkandraganov"+ lname + "@mail.bg");
            driver.FindElement(By.Id("phone")).Click();
            driver.FindElement(By.Id("phone")).SendKeys("0537654333");
            driver.FindElement(By.Id("comments")).Click();
            driver.FindElement(By.Id("comments")).SendKeys("zdr,kp");

            driver.FindElement(By.Id("create")).Click();


            driver.Navigate().GoToUrl("http://localhost:8080/");
            driver.FindElement(By.CssSelector(".home-page-icons > a:nth-child(3)")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("keyword")).Click();
            driver.FindElement(By.Id("keyword")).SendKeys("draganov" + lname);
            driver.FindElement(By.Id("search")).Click();
            //Thread.Sleep(1000);
            Assert.AreEqual("draganov" + lname, driver.FindElement(By.CssSelector(".lname > td")).Text);
            

        }
        [Test]
        public void CreateNewContact_invalid()
        {
            driver.FindElement(By.CssSelector(".home-page-icons > a:nth-child(2)")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("create")).Click();
            Assert.IsNotNull(driver.FindElement(By.CssSelector(".err")));
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }
    }
}