using System;
using System.Linq;

namespace VanillaRuleGenerator.Extensions
{
    public static class StringExtension
    {
        public static string Reverse(this string str)
        {
            var New = "";
            for (var i = str.Length - 1; i >= 0; i--)
            {
                New += str.Substring(i, 1);
            }
            return New;
        }
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new Exception("ArgumentNullException: input");
            }
            var chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
        public static string ReplaceAt(this string input, int index, string newChar)
        {
            if (input == null)
            {
                throw new Exception("ArgumentNullException: input");
            }
            if (newChar.Length == 0)
                throw new Exception("Replacement character must exist!");
            return input.ReplaceAt(index, newChar.ToCharArray()[0]);
        }

        public static string Capitalize(this string data)
        {
            return char.ToUpperInvariant(data.First()) + data.Substring(1).ToLowerInvariant();
        }
    }
}