using System.Text.RegularExpressions;

namespace ComparisonOfNumbVacancies__Technical_task_
{
    public class Parser
    {
        public static string FindStringInText(string text, string pattern, string deleteIdentifier)
        {
            Regex r = new Regex(pattern);
            string result = r.Match(text).Value;
            result = Regex.Replace(result, deleteIdentifier, ""); ;
            result = Regex.Replace(result, "\\.", "");
            return result;
        }

        public static string[] FindStringArrayInText(string text, string pattern, string deleteIdentifier)
        {
            Regex r = new Regex(pattern);
            string result = r.Match(text).Value;
            result = Regex.Replace(result, deleteIdentifier, ""); ;
            result = Regex.Replace(result, "\\.", "");
            result = Regex.Replace(result, " ", "");
            return result.Split(',');
        }
    }
}