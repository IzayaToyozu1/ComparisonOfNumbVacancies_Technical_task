using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using ComparisonOfNumbVacanciesBL.CommandsBrowser;

namespace ComparisonOfNumbVacanciesBL
{
    public class ComparisonOfNumbVacancies
    {
        private const string _sccSelectorButtonDepartment = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(2) > div > div > #sl";
        private const string _cssSelectorItemsDepartment = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(2) > div > div > div > a";

        private const string _cssSelectorKeywords = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(1) > input";

        private const string _cssSelectorButtonLanguage = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(3) > div > div > #sl";
        private const string _cssSelectorItemsLanguage = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(3) > div > div > div > div";

        private const string _cssSelectorButtonExperience = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(4) > div > div > #sl";
        private const string _cssSelectorItemsExperience = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(4) > div > div > div > a";

        private const string _cssSelectorButtonRegion = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(5) > div > div > #sl";
        private const string _cssSelectorItemsRegion = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-4 > div > div:nth-child(5) > div > div > div > a";

        private const string _cssSelectorItemVacancies = "#root > div > div.container-main.container-fluid > div > div.row.block-spacer-top > div.col-12.col-lg-8 > div > a";

        private const string siteVacancies = "https://careers.veeam.ru/vacancies";

        public event Action<string> ActionMessages;

        private BrowserChrome AppChrome;
        
        private CommandInstalFilterKeywords _filterKeywords;
        private CommandInstalFilterCheckBox _filterLanguage;
        private CommandInstalFilterComboBox _filterDepartment;
        private CommandInstalFilterComboBox _filterExperience;
        private CommandInstalFilterComboBox _filterRegion;

        private List<ICommandBrowser> command = new List<ICommandBrowser>();

        public ComparisonOfNumbVacancies()
        {
            AppChrome = new BrowserChrome();
            _filterKeywords = new CommandInstalFilterKeywords(AppChrome.Browser, _cssSelectorKeywords);
            _filterLanguage = new CommandInstalFilterCheckBox(AppChrome.Browser, _cssSelectorButtonLanguage, _cssSelectorItemsLanguage);
            _filterDepartment = new CommandInstalFilterComboBox(AppChrome.Browser, _sccSelectorButtonDepartment, _cssSelectorItemsDepartment);
            _filterExperience = new CommandInstalFilterComboBox(AppChrome.Browser, _cssSelectorButtonExperience, _cssSelectorItemsExperience);
            _filterRegion = new CommandInstalFilterComboBox(AppChrome.Browser, _cssSelectorButtonRegion, _cssSelectorItemsRegion);

            ICommandBrowser[] com = new ICommandBrowser[] { _filterKeywords, _filterLanguage, _filterDepartment, _filterExperience, _filterRegion };

            command.AddRange(com);

            _filterLanguage.FailMessage += CommandMessage;
            _filterDepartment.FailMessage += CommandMessage;
            _filterExperience.FailMessage += CommandMessage;
            _filterRegion.FailMessage += CommandMessage;
        }

        public void Start(int countComparVacancies)
        {
            OpenSiteVacancies();

            Thread.Sleep(1000);
            foreach (var com in command)
            {
                com.Execude();   
            }

            СomparisonCountVacancies(countComparVacancies);
        }

        public void InstalFilterVacancies(string keywords, string[] languages, string department, string experience, string region)
        {
            _filterKeywords.InstalFilterKeywords(keywords);
            _filterLanguage.InstalNewValueFilter(languages);
            _filterDepartment.InstalNewValueFilter(department);
            _filterExperience.InstalNewValueFilter(experience);
            _filterRegion.InstalNewValueFilter(region);
        }

        private void OpenSiteVacancies()
        {
            if (AppChrome.Browser.Url != siteVacancies)
                foreach (var site in AppChrome.Browser.WindowHandles)
                {
                    AppChrome.Browser.SwitchTo().Window(site);
                    if (AppChrome.Browser.Url == siteVacancies)
                        return;
                }
            AppChrome.OpenWebSite(siteVacancies);
        }

        private void СomparisonCountVacancies(int value)
        {
            int CountVacancies = AppChrome.Browser.FindElements(By.CssSelector(_cssSelectorItemVacancies)).Count;
            if (CountVacancies == value)
            {
                ActionMessages?.Invoke($"Количество вакансий на сайте по заданному фильтру соответствует ожидаемому результату {value}");
                return;
            }
            ActionMessages?.Invoke($"Количество вакансий на сайте по заданному фильтру не соответствует ожидаемому результату {value}");
        }

        private void CommandMessage(string message)
        {
            ActionMessages?.Invoke(message);
        }
    }
}