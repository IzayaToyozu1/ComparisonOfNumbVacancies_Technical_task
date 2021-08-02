using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ComparisonOfNumbVacanciesBL
{
    public class BrowserChrome
    {
        public IWebDriver Browser { get; set; }

        public BrowserChrome()
        {
            Browser = new ChromeDriver();
            Browser.Manage().Window.Maximize();
        }

        public void Close()
        {
            Browser.Quit();
        }

        public void OpenWebSite(string url)
        {
            Browser.Navigate().GoToUrl(url);
        }

    }
}