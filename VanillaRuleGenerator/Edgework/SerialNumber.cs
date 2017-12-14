using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VanillaRuleGenerator.Edgework
{
    public class SerialNumber
    {

        public static string Serial { get; private set; } = string.Empty;

        public static void SetSerialNumber(string serial)
        {
            Serial = serial.Trim().ToUpperInvariant();
        }

        public static bool IsSerialValid => Regex.IsMatch(Serial, "^[0-9A-Z][0-9A-Z][0-9][A-Z][A-Z][0-9]$");

        public static string[] SerialNumberLetters
        {
            get
            {
                var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                return (from letter in Serial where letters.Contains(letter) select letter.ToString()).ToArray();
            }
        }

        public static int[] SerialNumberDigits
        {
            get
            {
                var digits = "0123456789";
                return (from digit in Serial where digits.Contains(digit) select digits.IndexOf(digit)).ToArray();
            }
        }

        public static int SerialNumberSum => SerialNumberDigits.Sum();
        public static int LargestSerialDigit => SerialNumberDigits.Max();
        public static int SmallestSerialDigit => SerialNumberDigits.Min();

        public static int LastSerialDigit => SerialNumberDigits.Length > 0 ? SerialNumberDigits[SerialNumberDigits.Length - 1] : 0;
        public static int FirstSerialDigit => SerialNumberDigits.Length > 0 ? SerialNumberDigits[0] : 0;

        public static bool SerialNumberContainsVowel()
        {
            var vowels = new List<string> { "A", "E", "I", "O", "U" };
            for (var i = 0; i < 5; i++)
                if (Serial.ToUpper().Contains(vowels[i]))
                    return true;
            return false;
        }

        public static bool SerialNumberBeginsWithLetter()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return Serial.Trim().Length != 0
                   && letters.Contains(Serial.ToUpper().Substring(0, 1));
        }

        public static bool SerialNumberLastDigitOdd()
        {
            return LastSerialDigit % 2 == 1;
        }

        public static bool SerialNumberLastDigitEven()
        {
            return LastSerialDigit % 2 == 0;
        }

        public static int CountSerialNumberLetters()
        {
            return SerialNumberLetters.Length;
        }

        public static int CountSerialNumberDigits()
        {
            return SerialNumberDigits.Length;
        }

        public static bool DuplicateSerialCharacters()
        {
            for (var i = 0; i < Serial.Length; i++)
            for (var j = i + 1; j < Serial.Length; j++)
                if (Serial.Substring(i, 1) == Serial.Substring(j, 1))
                {
                    return true;
                }
            return false;
        }
    }
}