using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EventPlanner.API.Models
{
    public static class ValidationHelpers
    {
        public static string FormatWord(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                value = value.Trim().ToLower();
                value = char.ToUpper(value[0]) + value[1..];
            }

            return value;
        }

        public static string FormatLower(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                value = value.Trim().ToLower();
            }

            return value;
        }

        public static string FormatUpper(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                value = value.Trim().ToUpper();
            }

            return value;
        }

        public static string FormatFull(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                string[] words = value.Trim().Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = FormatWord(words[i]);
                }

                value = String.Join(" ", words);
            }

            return value;
        }

        public static string FormatBody(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                value = FormatWord(value);

                if (!(value.EndsWith('.') || value.EndsWith('!') || value.EndsWith('?')))
                {
                    value = $"{value}.";
                }
            }

            return value;
        }

        public static string FormatPhoneNumber(this string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                value = Regex.Replace(value, @"[\(\)\s\-]+", String.Empty);
            }

            return value;
        }
    }

    public class PastAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now;
        }
    }

    public class FutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }
}
