
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ComparisonOfNumbVacanciesBL.CommandsBrowser
{
    public class CommandInstalFilterCheckBox: ICommandBrowserFilterCheckBox
    {
        private IWebElement element1;
        private List<IWebElement> element2;

        private IWebDriver _browser;
        private string _cssSelectorButAccord;
        private string _cssSelectorValuesCheck;
        private string[] _valuesCheck;

        public event Action<string> FailMessage;

        public CommandInstalFilterCheckBox(IWebDriver browser, string cssSelectorButAccord, string cssSelectorValuesCheck)
        {
            _browser = browser;
            _cssSelectorButAccord = cssSelectorButAccord;
            _cssSelectorValuesCheck = cssSelectorValuesCheck;
            _valuesCheck = new[] {""};
        }

        public void Execude()
        {
            element1 = _browser.FindElement(By.CssSelector(_cssSelectorButAccord));

            if (_valuesCheck[0] == "")
                return;
            
            element1.Click();

            element2 = _browser.FindElements(By.CssSelector(_cssSelectorValuesCheck))
                ?.Where((e) =>
                {
                    foreach (var values in _valuesCheck)
                    {
                        if (e.Text == values)
                            return true;
                    }

                    return false;
                })
                .ToList();
            if (element2 == null)
            {
                FailMessage.Invoke("Значения фильтра введены неверно");
                return;
            }

            foreach (var e in element2)
            {
                e.Click();   
            }
            element1.Click();
        }

        public void InstalNewValueFilter(string[] values)
        {
            _valuesCheck = values ?? new string[] {""};
        }
    }
}
