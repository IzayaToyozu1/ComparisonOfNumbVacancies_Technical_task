using OpenQA.Selenium;

namespace ComparisonOfNumbVacanciesBL.CommandsBrowser
{
    public class CommandInstalFilterKeywords: ICommandBrowser
    {
        private IWebElement elementKeyword;
        private IWebDriver _browser;
        private string _keywords;
        private string _cssSelectorKeywords;

        public CommandInstalFilterKeywords(IWebDriver browser, string cssSelector)
        {
            _browser = browser;
            _cssSelectorKeywords = cssSelector;
            _keywords = "";
        }

        public void Execude()
        {
            if (_keywords == "")
                return;
            elementKeyword = _browser.FindElement(By.CssSelector(_cssSelectorKeywords));
            elementKeyword.SendKeys(_keywords);
        }

        public void InstalFilterKeywords(string value)
        {
            _keywords = value ?? "";
        }
    }
}
