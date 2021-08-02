namespace ComparisonOfNumbVacanciesBL.CommandsBrowser
{
    public interface ICommandBrowserFilterKeywords: ICommandBrowser
    {
        public void InstalNewValueFilter(string values);
    }
}