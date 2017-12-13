using System.Collections.Generic;
using System.Linq;
using KTaNE_Helper.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    public class SkewedSlots
    {
        private bool InFibonacci(int number)
        {
            int fibonacci = 1;
            int cur = 0;
            int prev = 0;
            do
            {
                if (number == fibonacci)
                    return true;
                cur = fibonacci;
                fibonacci += prev;
                prev = cur;
            } while (fibonacci <= number);
            return false;
        }

        private int NextFibonacci(int number)
        {
            if (number == 1) return 1;  //Special case to comply with skewed slot rules of First encountered digit.
            int fibonacci = 1;
            int cur = 0;
            int prev = 0;
            do
            {
                cur = fibonacci;
                fibonacci += prev;
                prev = cur;
            } while (fibonacci < number);
            return fibonacci + prev;
        }

        private static bool IsPrime(int num)
        {
            var prime = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            return prime.Contains(num);
        }

        public string GetAnswer(string originalNumbers)
        {
            if (!SerialNumber.IsSerialValid) return "";
            if (originalNumbers.Trim().Length != 3) return "";
            var originalDigitsInt = new int[3];
            var newDigits = new int[3];

            //ALL Slotss
            for (var i = 0; i < 3; i++)
            {
                if (!int.TryParse(originalNumbers[i].ToString(), out originalDigitsInt[i]))
                    return "";
                if (originalDigitsInt[i] == 2)
                    newDigits[i] = 5;
                else if (originalDigitsInt[i] == 7)
                    newDigits[i] = 0;
                else
                    newDigits[i] = originalDigitsInt[i];

                newDigits[i] += Indicators.LitIndicators.Length;
                newDigits[i] -= Indicators.UnlitIndicators.Length;

                if (newDigits[i] % 3 == 0)
                    newDigits[i] += 4;
                else if ((newDigits[i] > 7))
                    newDigits[i] *= 2;
                else if ((newDigits[i] < 3) && (newDigits[i] % 2 == 0))
                    newDigits[i] /= 2;
                else if (!PortPlate.IsPortPresent(PortTypes.PS2) && !PortPlate.IsPortPresent(PortTypes.StereoRCA))
                    newDigits[i] = originalDigitsInt[i] + Batteries.TotalBatteries;
            }

            //1st Slot
            if ((newDigits[0] > 5) && (newDigits[0] % 2 == 0))
                newDigits[0] /= 2;

            else if (IsPrime(newDigits[0]))
                newDigits[0] += SerialNumber.LastSerialDigit;
            else if (PortPlate.IsPortPresent(PortTypes.Parallel))
                newDigits[0] *= -1;
            else if (originalDigitsInt[1] % 2 == 0)
                newDigits[0] -= 2;

            //2nd Slot
            if (!Indicators.UnlitIndicators.Contains("BOB"))
            {
                if (newDigits[1] == 0)
                    newDigits[1] += originalDigitsInt[0];
                else if (InFibonacci(newDigits[1]))
                    newDigits[1] += NextFibonacci(newDigits[1]);
                else if (newDigits[1] >= 7)
                    newDigits[1] += 4;
                else
                    newDigits[1] *= 3;
            }

            //3rd Slot
            if (PortPlate.IsPortPresent(PortTypes.Serial))
                newDigits[2] += SerialNumber.LargestSerialDigit;
            else if (originalDigitsInt[2] != originalDigitsInt[1] && originalDigitsInt[2] != originalDigitsInt[0])
            {
                var binary = new int[]
                {
                    0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2
                };
                if (newDigits[2] >= 5)
                    newDigits[2] = binary[originalDigitsInt[2]];
                else
                    newDigits[2] += 1;
            }

            for (var i = 0; i < 3; i++)
            {
                while (newDigits[i] >= 10)
                    newDigits[i] -= 10;
                while (newDigits[i] < 0)
                    newDigits[i] += 10;
            }

            return $"{newDigits[0]}{newDigits[1]}{newDigits[2]}";
        }
    }
}