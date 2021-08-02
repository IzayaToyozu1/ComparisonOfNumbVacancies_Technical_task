namespace ComparisonOfNumbVacanciesBL.CommandsBrowser
{
    public interface ICommandBrowserFilterCheckBox: ICommandBrowser
    {
        public void InstalNewValueFilter(string[] values);
    }
}