using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;

namespace wiki_val_QA_tests
{
    public class Homepage_Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://wiki-val.herokuapp.com");
        }

        [Test]
        public void Test_Load_Site()
        {
            Assert.AreEqual("Encyclopedia", driver.Title);

            driver.FindElement(By.LinkText("Create New Page")).Click();
            Assert.AreEqual("Create New Page", driver.Title);
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Random Page")).Click(); 
            Assert.IsTrue(driver.FindElement(By.Id("edit-link")).Displayed);
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Home")).Click();
            Assert.AreEqual("Encyclopedia", driver.Title);

        }

        [Test]
        public void Test_Search()
        {
            driver.FindElement(By.Id("search")).Click();
            driver.FindElement(By.Id("search")).SendKeys("Python");
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            Assert.AreEqual(driver.FindElement(By.CssSelector("h1")).Text, "Python");
        }

        [Test]
        public void Test_Search_invalid()
        {
            driver.FindElement(By.Id("search")).Click();
            driver.FindElement(By.Id("search")).SendKeys("qwertyuiopasdfghjkl12345");
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            Assert.AreEqual(driver.FindElement(By.CssSelector("p")).Text, "The topic you are lookig for doesn't exist.");
        }

        [Test]
        public void Test_Search_Suggest()
        {
            driver.FindElement(By.Id("search")).Click();
            driver.FindElement(By.Id("search")).SendKeys("Pyt");
            driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            Assert.AreEqual(driver.FindElement(By.LinkText("Python")).Text, "Python");
        }

        [Test]
        public void Test_randomPage()
        {
            //gets all the pages
            var pages = driver.FindElements(By.Id("index-list-item"));
            List<string> pagesText = new List<string>();
            foreach (var p in pages)
            {
                pagesText.Add(p.Text);
            }

            driver.FindElement(By.LinkText("Random Page")).Click();
            string titleName = driver.Title;
            //check to see if page exists
            foreach (string p in pagesText)
            {
                if (titleName == p) {
                    Assert.IsTrue(true);
                    break;
                }
            }
        }
        [TearDown]

        protected void TearDown()
        {
            driver.Quit();
        }
    }
}