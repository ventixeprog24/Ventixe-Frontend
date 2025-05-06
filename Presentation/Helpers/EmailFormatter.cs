using System.Text.RegularExpressions;

namespace Presentation.Helpers
{
    public class EmailFormatter
    {
        public static string MaskEmail(string email)
        {
            string pattern = @"(?<=[\w]{1})[\w\-._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(email, pattern, m => new string('*', m.Length));
            return result;
        }
    }
}
