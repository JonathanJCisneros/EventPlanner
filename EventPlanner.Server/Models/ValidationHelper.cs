using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Server.Models
{
    public static class ValidationHelper
    {
        public static string FormatWord(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim().ToLower();
                value = char.ToUpper(value[0]) + value[1..];
            }

            return value;
        }

        public static string FormatLower(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim().ToLower();
            }

            return value;
        }

        public static string FormatUpper(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim().ToUpper();
            }

            return value;
        }

        public static string FormatFull(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string[] words = value.Trim().Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = FormatWord(words[i]);
                }

                value = string.Join(" ", words);
            }

            return value;
        }

        public static string FormatBody(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = FormatWord(value);

                if (!(value.EndsWith('.') || value.EndsWith('!') || value.EndsWith('?')))
                {
                    value = $"{value}.";
                }
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
