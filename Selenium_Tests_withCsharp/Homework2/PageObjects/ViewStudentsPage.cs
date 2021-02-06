using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Linq;

namespace NUnitPOM.PageObjects

{
    public class ViewStudentsPage : BasePage
    {
        public ViewStudentsPage(IWebDriver driver) : base(driver){ }

        public override string PageUrl => "https://mvc-app-node-express.nakov.repl.co/students";

        public ReadOnlyCollection<IWebElement> ListItemsStudents => Driver.FindElements(By.CssSelector("body > ul > li"));

        public string[] GetRegisteredStudents(){
            var elementsStudents = this.ListItemsStudents.Select(s => s.Text).ToArray();
            return elementsStudents;
        }
    }
}
