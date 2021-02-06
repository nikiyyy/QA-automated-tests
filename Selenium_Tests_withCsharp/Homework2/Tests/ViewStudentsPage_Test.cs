using NUnit.Framework;
using NUnitPOM.PageObjects;

namespace NUnitPOM.Tests
{
    public class ViewStudentsPage_Test : BaseTest
    {
        [Test]
        public void ViewStudentsPage_Content()
        {
            var page = new ViewStudentsPage(driver);
            page.Open();
            Assert.AreEqual("Students", page.GetPageTitle());
            Assert.AreEqual("Registered Students", page.GetPageHeadingText());
            
            var students = page.GetRegisteredStudents();
            foreach (string el in students)
            {
                Assert.IsTrue(el.IndexOf("(") > 0);
                Assert.IsTrue(el.LastIndexOf(")") == el.Length - 1);
            }
        }
        [Test]
        public void ViewStudentsLink_View()
        {
            var page = new ViewStudentsPage(driver);
            page.Open();
            page.LinkStudents.Click();
            Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());
        }
        [Test]
        public void HViewStudentsLink_Add()
        {
            var page = new ViewStudentsPage(driver);
            page.Open();
            page.LinkAddStudents.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
        }
        [Test]
        public void ViewStudentsLink_Home()
        {
            var page = new ViewStudentsPage(driver);
            page.Open();
            page.LinkHomePage.Click();//proveri Xpath ili homepage klasa
            Assert.IsTrue(new HomePage(driver).IsOpen());

        }

    }
}