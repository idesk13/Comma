using System;
using System.Globalization;
using System.Text;

namespace Comma.BusinessLogic
{
    public static class WordExtensions
    {
        public static string RemoveDiacritis(this string word)
        {
            var normalizedString = word.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveCharacter(string input)
        {

            var charsToRemove = new string[] { "@", ",", "^", "1", "2", "3", "!", "*", " " };
            foreach (var c in charsToRemove)
            {
                input = input.Replace(c, string.Empty);
            }

            return input;
        }
    }
}
