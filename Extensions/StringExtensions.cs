using System.Text;

namespace Decksplain.Extensions;

public static class StringExtensions
{
    public static string Slugify(this string phrase)
    {
        if (string.IsNullOrEmpty(phrase))
            return string.Empty;

        var stringBuilder = new StringBuilder();

        foreach (char c in phrase)
        {
            if (char.IsLetterOrDigit(c))
            {
                stringBuilder.Append(char.ToLower(c));
            }
            else if (char.IsWhiteSpace(c))
            {
                stringBuilder.Append('-');
            }
            // Ignore other characters.
        }

        return stringBuilder.ToString();
    }
}
