using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTaNE_Helper
{
    internal class NumberPad
    {
        public NumberPad(string colors)
        {
            _colors = colors.ToUpper();
        }

        private readonly string _colors;

        private const string Wheel = "22468313395143690979890789940526034176635285026086097984297491480871855832860082003490389675061692920733696061238335";
        private string _workingCode;

        private const int ColorWhite = 0;
        private const int ColorGreen = 1;
        private const int ColorYellow = 2;
        private const int ColorBlue = 3;
        private const int ColorRed = 4;
        public readonly int ColorAll = 5;

        private static bool ArrayContains<T>(IEnumerable<T> array, T query)
        {
            return array.Any(value => EqualityComparer<T>.Default.Equals(value, query));
        }

        private int GetButtonColor(int number)
        {
            var lookup = new[] { 9, 6, 7, 8, 3, 4, 5, 0, 1, 2 };
            const string clookup = "WGYBR";
            return clookup.IndexOf(_colors.Substring(lookup[number], 1), StringComparison.Ordinal);
        }

        public int GetColorCount(int c)
        {
            var j = 0;

            if (c == ColorAll)
            {
                while (c-- > 0)
                    j += GetColorCount(c);
                return j;
            }

            var color = ("WGYBR").Substring(c, 1);
            for (var i = 0; i < 10; i++)
                if (_colors.Substring(i, 1) == color)
                    j++;
            return j;
        }


        private int GetPathForLevel(int level)
        {
            //print ("getting path for level " + level);
            switch (level)
            {
                case 0:
                    if (GetColorCount(ColorYellow) >= 3)
                        return 0;
                    if (
                        ArrayContains(new[] { ColorWhite, ColorBlue, ColorRed }, GetButtonColor(4)) &&
                        ArrayContains(new[] { ColorWhite, ColorBlue, ColorRed }, GetButtonColor(5)) &&
                        ArrayContains(new[] { ColorWhite, ColorBlue, ColorRed }, GetButtonColor(6)))
                        return 1;
                    return SerialNumber.SerialNumberContainsVowel()
                        ? 2
                        : 3;
                case 1:
                    if (GetColorCount(ColorBlue) >= 2 && GetColorCount(ColorGreen) >= 3)
                        return 0;
                    if (GetButtonColor(5) != ColorBlue && GetButtonColor(5) != ColorWhite)
                        return 1;
                    if (PortPlate.CountTotalPorts() < 2)
                        return 2;
                    if (GetButtonColor(7) == ColorGreen || GetButtonColor(8) == ColorGreen || GetButtonColor(9) == ColorGreen)
                        SubtractDigit(0);
                    return 3;

                case 2:

                    return GetColorCount(ColorWhite) > 2 && GetColorCount(ColorYellow) > 2
                        ? 0
                        : 1; // remember to reverse the code thus far

                case 3:

                    return GetColorCount(ColorYellow) <= 2
                        ? 0 // remember to add 1 to each digit
                        : 1;
                default:
                    return -1;
            }
        }

        private static string[] PickFrom(string input, int choice, int choices)
        {
            // string[]{
            //  digit
            //  containing stuff
            // }

            var ret = new string[2];
            if (choice < 0 || choice >= choices)
            {
                throw new Exception("NUMBER PAD: Choice out of range!");
            }
            if (input.Length % choices != 0)
            {

                throw new Exception("NUMBER PAD: While trying to pick a portion of the code wheel, the string's length (" + input.Length + ") wasn't divisible by the choice count (" + choices + ")!");
            }
            var idx = input.Length / choices * choice;
            ret[0] = input.Substring(idx, 1);
            ret[1] = input.Substring(idx + 1, input.Length / choices - 1);
            //print ("the chosen path is " + Choice + " with " + Choices + " choices, the input is \"" + Input + "\", the number is " + ret [0] + ", and the rest is \"" + ret [1] + "\"");
            return ret;

        }

        private void AddDigit(int digit)
        {
            var text = _workingCode;
            var num = int.Parse(text.Substring(digit, 1));
            num++;
            if (num > 9)
                num = 0;
            _workingCode = _workingCode.ReplaceAt(digit, num.ToString());
        }

        private void SubtractDigit(int digit)
        {
            var text = _workingCode;
            var num = int.Parse(text.Substring(digit, 1));
            num--;
            if (num < 0)
                num = 9;
            _workingCode = _workingCode.ReplaceAt(digit, num.ToString());
        }

        public string GetCorrectCode()
        {

            const string cur = Wheel;
            _workingCode = "";

            var path = GetPathForLevel(0);
            var status = PickFrom(cur, path, 4);
            _workingCode = status[0];
            //print ("workingcode is " + WorkingCode);

            path = GetPathForLevel(1);
            status = PickFrom(status[1], path, 4);
            _workingCode += status[0];
            //print ("workingcode is " + WorkingCode);

            path = GetPathForLevel(2);
            status = PickFrom(status[1], path, 2);
            _workingCode += status[0];
            if (path == 1)
            {
                //print ("took second path, reversing code");
                _workingCode = _workingCode.Reverse();
            }
            //print ("workingcode is " + WorkingCode);

            path = GetPathForLevel(3);
            status = PickFrom(status[1], path, 2);
            _workingCode += status[0];
            if (path == 0)
            {
                //print ("took first path, adding 1 to all digits");
                for (var i = 0; i < 4; i++)
                {
                    AddDigit(i);
                }
            }
            //print ("workingcode is " + WorkingCode);

            var notMet = true;
            if (SerialNumber.SerialNumberLastDigitEven() )
            {
                //print ("serial even, swapping 1 and 3");
                var old = _workingCode.Substring(2, 1);
                _workingCode = _workingCode.ReplaceAt(2, _workingCode.Substring(0, 1));
                _workingCode = _workingCode.ReplaceAt(0, old);
                notMet = false;
            }
            if (Batteries.TotalBatteries % 2 == 1)
            {
                //print ("battery count odd, swapping 2 and 3");
                var old = _workingCode.Substring(2, 1);
                _workingCode = _workingCode.ReplaceAt(2, _workingCode.Substring(1, 1));
                _workingCode = _workingCode.ReplaceAt(1, old);
                notMet = false;
            }
            if (notMet)
            {
                //print ("neither conditions met, swapping 1 and 4");
                var old = _workingCode.Substring(3, 1);
                _workingCode = _workingCode.ReplaceAt(3, _workingCode.Substring(0, 1));
                _workingCode = _workingCode.ReplaceAt(0, old);
            }
            //print ("workingcode is " + WorkingCode);

            var sum = 0;
            for (var i = 0; i < 4; i++)
            {
                sum += int.Parse(_workingCode.Substring(i, 1));
            }
            if (sum % 2 == 0)
            {
                //print ("sum is even reversing");
                _workingCode = _workingCode.Reverse();
            }
            //print ("workingcode is FINALLY " + WorkingCode);

            return _workingCode;
        }
    }
}
