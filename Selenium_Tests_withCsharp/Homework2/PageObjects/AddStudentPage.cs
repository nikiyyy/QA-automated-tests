using OpenQA.Selenium;

namespace NUnitPOM.PageObjects
{
    public class AddStudentPage : BasePage
    {
        public AddStudentPage(IWebDriver driver) : base(driver){
        }

        public override string PageUrl =>
            "https://mvc-app-node-express.nakov.repl.co/add-student";

        public IWebElement FieldName =>
            Driver.FindElement(By.CssSelector("input#name"));

        public IWebElement FieldEmail =>
            Driver.FindElement(By.CssSelector("input#email"));

        public IWebElement ButtonSubmit =>
            Driver.FindElement(By.CssSelector("button[type='submit']"));

        public IWebElement ElementErrorMsg =>
            Driver.FindElement(By.XPath("//div[contains(@style,'background:red')]"));

        public void AddStudent(string name, string email)
        {
            this.FieldName.SendKeys(name);
            this.FieldEmail.SendKeys(email);
            this.ButtonSubmit.Click();
        }
    }
}
