using NUnit.Framework;
using NUnitPOM.PageObjects;
using System;

namespace NUnitPOM.Tests{
    public class AddStudentsPage_Test : BaseTest{
        [Test]
        public void AddStudentsPage_Content(){
            var page = new AddStudentPage(driver);
            page.Open();
            Assert.AreEqual("Add Student", page.GetPageTitle());
            Assert.AreEqual("Register New Student", page.GetPageHeadingText());
            Assert.AreEqual("", page.FieldName.Text);
            Assert.AreEqual("", page.FieldEmail.Text);
            Assert.AreEqual("Add", page.ButtonSubmit.Text);
        }

        [Test]
        public void AddValidStudent_Test(){
            var page = new AddStudentPage(driver);
            page.Open();
            Random rnd = new Random();
            string name = "" + rnd.Next(10000000);
            string email = "" + rnd.Next(10000000) + "@test.com";
            page.AddStudent(name, email);
            var viewStudentsPage = new ViewStudentsPage(driver);
            Assert.IsTrue(viewStudentsPage.IsOpen());
            var students = viewStudentsPage.GetRegisteredStudents();
            string newStudent = name + " (" + email + ")";
            Assert.Contains(newStudent, students);
        }

        [Test]
        public void AddInvalidStudent_Test(){
            var page = new AddStudentPage(driver);
            page.Open();
            page.AddStudent("", "");
            Assert.IsTrue(page.IsOpen());
            Assert.IsTrue(page.ElementErrorMsg.Text.Contains("Cannot add student"));
        }
        [Test]
        public void AddStudentLink_View()
        {
            var page = new AddStudentPage(driver);
            page.Open();
            page.LinkStudents.Click();
            Assert.IsTrue(new ViewStudentsPage(driver).IsOpen());
        }
        [Test]
        public void AddStudentLink_Add()
        {
            var page = new AddStudentPage(driver);
            page.Open();
            page.LinkAddStudents.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
        }
        [Test]
        public void AddStudentLink_Home()
        {
            var page = new AddStudentPage(driver);
            page.Open();
            page.LinkHomePage.Click();//proveri Xpath ili homepage klasa
            Assert.IsTrue(new HomePage(driver).IsOpen());
        }

    }
}