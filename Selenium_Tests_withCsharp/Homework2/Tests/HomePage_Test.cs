using NUnit.Framework;
using NUnitPOM.PageObjects;

namespace NUnitPOM.Tests
{
    public class HomePage_Test : BaseTest
    {
        [Test]
        public void HomePage_Content()
        {
            var page = new PageObjects.HomePage(driver);
            page.Open();
            Assert.AreEqual("MVC Example", page.GetPageTitle());
            Assert.AreEqual("Students Registry", page.GetPageHeadingText());
            Assert.Pass();
        }

        [Test]
        public void HomePageLink_View()
        {
            var page = new HomePage(driver);
            page.Open();
            page.LinkStudents.Click();
            Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());
        }
        [Test]
        public void HomePageLink_Add()
        {
            var page = new HomePage(driver);
            page.Open();
            page.LinkAddStudents.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
        }
        [Test]
        public void HomePageLink_Home()
        {
            var page = new HomePage(driver);
            page.Open();
            page.LinkHomePage.Click();//proveri Xpath ili homepage klasa
            Assert.IsTrue(new HomePage(driver).IsOpen());
            //Assert.IsTrue(BasePage.IsOpen());
            //Assert.IsTrue(viewStudentsPage.IsOpen());
        }

    }



}