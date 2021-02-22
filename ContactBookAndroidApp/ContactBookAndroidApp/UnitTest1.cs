using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace ContactBookAndroidApp
{

    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Android" };//device - Pixel 2API 29 - resolution: 1080x1920 420dpi - Android 10.0 - CPU x86 - size disk 9.5 GB
            appiumOptions.AddAdditionalCapability("app", @"C:\Users\kolio\Downloads\contactbook-androidclient.apk"); // Don't forget to change to your location
            driver = new AndroidDriver<AndroidElement>(
                new Uri("http://[::1]:4723/wd/hub"), appiumOptions);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        [Test]
        public void Test1()
        {   
            var apiBar = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            apiBar.Click();
            var searchBar = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchBar.SendKeys("steve");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            string fname = driver.FindElementById("contactbook.androidclient:id/textViewFirstName").Text;
            string lname = driver.FindElementById("contactbook.androidclient:id/textViewLastName").Text;

            Assert.AreEqual("Steve Jobs", fname + " " + lname);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}