using System.Collections.Generic;
using VanillaRuleGenerator.Extensions;

namespace KTaNE_Helper.Modules.Modded
{
    public static class Switches
    {
        private static bool IsSwitchStateIllegal(string state)
        {
            var illegalStates = new List<string>
            {
                "00100","01011","01111","10010","10011","10111","11000","11010","11100","11110"
            };
            return illegalStates.Contains(state);
        }

        private static string GetSolution(ref string current, string desired, string disallowed = "")
        {
            var solution = "";
            var flag = false;
            for (var i = 0; i < 5 && current != desired; i++)
            {
                var c = current.Substring(i, 1);
                var d = desired.Substring(i, 1);
                if (c == d) continue;
                var temp = current.ReplaceAt(i, d);
                if (IsSwitchStateIllegal(temp) || temp == disallowed)
                {
                    flag = true;
                    continue;
                }
                current = temp;
                solution += (i + 1).ToString();
                if (!flag) continue;
                flag = false;
                i = -1;
            }

            return solution;
        }

        public static string Solve(string current, string desired)
        {
            var validStates = new List<string>();

            for (var i = 0; i < 32; i++)
            {
                var t = (i & 16) == 16 ? "1" : "0";
                t += (i & 8) == 8 ? "1" : "0";
                t += (i & 4) == 4 ? "1" : "0";
                t += (i & 2) == 2 ? "1" : "0";
                t += (i & 1) == 1 ? "1" : "0";
                if (!IsSwitchStateIllegal(t))
                    validStates.Add(t);
            }

            if (IsSwitchStateIllegal(current))
            {
                return @"Current is Illegal";
            }
            if (IsSwitchStateIllegal(desired))
            {
                return @"Desired is Illegal";
            }

            var currenttemp = current;
            var solution = GetSolution(ref current, desired);
            if (current == desired)
            {
                return solution;
            }

            var solutions = new List<string>();
            foreach (var intermmediate in validStates)
            {
                current = currenttemp;

                if (desired == intermmediate) continue;
                solution = GetSolution(ref current, intermmediate);

                if (current != intermmediate) continue;
                solution += GetSolution(ref current, desired);

                if (current == desired) solutions.Add(solution);
            }
            var x = 999;
            var y = "";
            foreach (var s in solutions)
            {
                if (s.Length >= x) continue;
                x = s.Length;
                y = s;
            }
            return y == "" ? @"Not Solved" : y;
        }
    }
}