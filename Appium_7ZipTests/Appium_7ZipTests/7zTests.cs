using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using NUnit.Framework;

namespace Appium_7ZipTests
{
    public class Tests
    {

        private const string AppiumServerUrl = "http://[::1]:4723/wd/hub";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> desktopDriver;
        private string workDir;

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", @"C:\Program Files\7-Zip\7zFM.exe");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUrl), appiumOptions);

            var appiumOptionsDesktop = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(
                new Uri(AppiumServerUrl), appiumOptionsDesktop);

            workDir = Directory.GetCurrentDirectory() + @"\workdir";
            if (Directory.Exists(workDir))
                Directory.Delete(workDir, true);
            Directory.CreateDirectory(workDir);
        }

        [Test]
        public void Test1()
        {
            var textBoxLocateFolder = driver.FindElementByXPath("/Window/Pane/Pane/ComboBox/Edit[@ClassName='Edit']");
            textBoxLocateFolder.SendKeys(@"C:\Program Files\7-Zip\");
            textBoxLocateFolder.SendKeys(Keys.Enter);

            var listBoxFiles = driver.FindElementByAccessibilityId("1001");
            listBoxFiles.SendKeys(Keys.Control + 'a');

            var Addbutton = driver.FindElementByXPath("/Window/ToolBar/Button[@Name='Add']");
            Addbutton.Click();

            Thread.Sleep(1000);

            var windowAddToArchive = desktopDriver.FindElementByName("Add to Archive");

            var textBoxArchiveName = windowAddToArchive.FindElementByXPath("/Window/ComboBox/Edit[@Name='Archive:']");
            Random rnd = new Random();
            string archiveFileName = workDir + "\\" + rnd.Next(10000000) + ".7z";
            textBoxArchiveName.SendKeys(archiveFileName);

            var DictSize = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Dictionary size:']");
            DictSize.SendKeys(Keys.End);

            var WordSize = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Word size:']");
            WordSize.SendKeys(Keys.End);

            var buttonAddToArchiveOK = windowAddToArchive.FindElementByXPath("/Window/Button[@Name='OK']");
            buttonAddToArchiveOK.Click();

            Thread.Sleep(1000);

            textBoxLocateFolder.SendKeys(archiveFileName + Keys.Enter);

            var buttonExtract = driver.FindElementByXPath("/Window/ToolBar/Button[@Name='Extract']");
            buttonExtract.Click();

            var buttonExtractOK = driver.FindElementByXPath("/Window/Window/Button[@Name='OK']");
            buttonExtractOK.Click();
            Thread.Sleep(1000);

            string executable7ZipOriginal = @"C:\Program Files\7-Zip\7zFM.exe";
            string executable7ZipExtracted = workDir + @"\7zFM.exe";
            FileAssert.AreEqual(executable7ZipOriginal, executable7ZipExtracted);

        }

        [OneTimeTearDown]
        public void Shutdown()
        {
            driver.Quit();
            desktopDriver.Quit();
        }
    }
}