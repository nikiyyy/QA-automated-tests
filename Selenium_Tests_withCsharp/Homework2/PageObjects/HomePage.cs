using OpenQA.Selenium;

namespace NUnitPOM.PageObjects
{
    internal class HomePage : BasePage{
        public HomePage(IWebDriver driver) : base(driver){}

        public override string PageUrl => "https://mvc-app-node-express.nakov.repl.co/";

        public IWebElement ElementStudentsCount => Driver.FindElement(By.CssSelector("body > p > b"));

        public int GetStudentsCount(){
            string studentsCountText = this.ElementStudentsCount.Text;
            return int.Parse(studentsCountText);
        }
    }

}