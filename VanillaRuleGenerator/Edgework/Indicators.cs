using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VanillaRuleGenerator.Edgework
{
    public class Indicators
    {

        private static List<List<string>> _indicators = new List<List<string>>
        {
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>()
        };


        public static void SetIndicators(string lit, string unlit)
        {
            foreach (var list in _indicators)
                list.Clear();


            var u = unlit.ToUpperInvariant()
                .Split(new[] { ",", ":", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Black].AddRange(u.Where(indicator => Regex.IsMatch(indicator, "^[A-Z][A-Z][A-Z]$")));

            var l = lit.ToUpperInvariant()
                .Split(new[] { ",", ":", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            KMBombInfoExtensions.KnownIndicatorColors color = KMBombInfoExtensions.KnownIndicatorColors.White;
            foreach (var indicator in l)
            {
                switch (indicator)
                {
                    case "K":
                    case "U":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Black;
                        continue;
                    case "W":
                    case "L":
                        continue;
                    case "A":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Gray;
                        continue;
                    case "R":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Red;
                        continue;
                    case "O":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Orange;
                        continue;
                    case "Y":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Yellow;
                        continue;
                    case "G":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Green;
                        continue;
                    case "B":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Blue;
                        continue;
                    case "P":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Purple;
                        continue;
                    case "M":
                        color = KMBombInfoExtensions.KnownIndicatorColors.Magenta;
                        continue;

                    default:
                        if (!Regex.IsMatch(indicator, "^[A-Z][A-Z][A-Z]$")) break;
                        _indicators[(int) color].Add(indicator);
                        break;
                }
                color = KMBombInfoExtensions.KnownIndicatorColors.White;
            }



        }

        public static string[] LitIndicators
        {
            get
            {
                var lit = new List<string>();
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.White]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Red]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Orange]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Yellow]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Green]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Blue]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Purple]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Magenta]);
                lit.AddRange(_indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Gray]);

                return lit.ToArray();
            }
        }

        public static string[] UnlitIndicators => _indicators[(int)KMBombInfoExtensions.KnownIndicatorColors.Black].ToArray();

        public static bool IsIndicatorLit(string indicator)
        {
            return LitIndicators.Any(i => indicator.ToUpperInvariant() == i);
        }

        public static string[] GetColoredIndicator(int color)
        {
            if (color < 0 || color >= _indicators.Count) return new string[0];
            return _indicators[color].ToArray();
        }

        public static bool IsIndicatorUnlit(string indicator)
        {
            return UnlitIndicators.Any(i => indicator.ToUpperInvariant() == i);
        }

        public static bool IsIndicatorPresent(string indicator)
        {
            return IsIndicatorLit(indicator) || IsIndicatorUnlit(indicator);
        }

        
    }
}