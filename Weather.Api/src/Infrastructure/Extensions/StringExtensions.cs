using System.Text.RegularExpressions;

namespace Weather.Api.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsBlank(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsZipCode(this string value)
        {
            const string pattern = @"\d{5}(-\d{4})?";
            var regex = new Regex(pattern);

            return regex.IsMatch(value);
        }
    }
}