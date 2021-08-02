using System;
using System.Linq;
using OpenQA.Selenium;

namespace ComparisonOfNumbVacanciesBL.CommandsBrowser
{
    public class CommandInstalFilterComboBox : ICommandBrowserFilterComboBox
    {
        private IWebElement element1;
        private IWebElement element2;

        private IWebDriver _browser;
        private string _cssSelectorButAccord;
        private string _cssSelectorValuesAccord;
        private string _valueFilter;

        public event Action<string> FailMessage;

        public CommandInstalFilterComboBox(IWebDriver browser, string cssSelectorButAccord, string cssSelectorValuesAccord)
        {
            _browser = browser;
            _cssSelectorButAccord = cssSelectorButAccord;
            _cssSelectorValuesAccord = cssSelectorValuesAccord;
            _valueFilter = "";
        }
        public void Execude()
        {
            if (_valueFilter == "")
                return;
            element1 = _browser.FindElement(By.CssSelector(_cssSelectorButAccord));
            element1.Click();

            element2 = _browser.FindElements(By.CssSelector(_cssSelectorValuesAccord))
                ?.FirstOrDefault(e => e.Text == _valueFilter);
            if (element2 == null)
            {
                FailMessage.Invoke("Значения фильтра введены неверно");
                return;
            }
            element2.Click();
        }

        public void InstalNewValueFilter(string value)
        {
            _valueFilter = value ?? "";
        }
    }
}