using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VanillaRuleGenerator.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    public class MorseAMaze
    {
        private MorseAMaze() { }
        public static MorseAMaze Instance => Nested.instance;
        // ReSharper disable once ClassNeverInstantiated.Local
        private class Nested
        {
            static Nested() { }
            // ReSharper disable once InconsistentNaming
            internal static readonly MorseAMaze instance = new MorseAMaze();
        }

        private readonly KMBombInfo _bombInfo = new KMBombInfo();

        private readonly Dictionary<int, string> _directWords = new Dictionary<int, string>
        {
            {0, "KABOOM"},{1, "UNICORN"},{2, "QUEBEC"},
            {3, "BASHLY"},{4, "SLICK"},{5, "VECTOR"},
            {6, "FLICK"},{7, "TIMWI"},{8, "STROBE"},
            {9, "BOMBS"},{10, "BRAVO"},{11, "LAUNDRY"},
            {12, "BRICK"},{13, "KITTY"},{14, "HALLS"},
            {15, "STEAK"},{16, "BREAK"},{17, "BEATS"},

            {-1, "SHELL"},{-2, "LEAKS"},{-3, "STRIKE"},
            {-4, "HELLO"},{-5, "VICTOR"},{-6, "ALIEN3"},
            {-7, "BISTRO"},{-8, "TANGO"},{-9, "TIMER"},
            {-10, "BOXES"},{-11, "TRICK"},{-12, "PENGUIN"},
            {-13, "STING"},{-14, "ELIAS"},{-15, "KTANE"},
            {-16, "MANUAL"},{-17, "ZULU"},{-18, "NOVEMBER"},
        };

        public int GetMaze(string code, string param, Label parameterName)
        {
            parameterName.Text = string.Empty;
            code = code.ToLowerInvariant();

            var wordList = _directWords.Select(x => Form1.WordToMorseCode(x.Value.ToLowerInvariant(), x.Key, true)).Where(word => word.Contains(code)).ToList();
            if (wordList.Count != 1)
            {
                return -1;
            }

			if (!int.TryParse(param, out int paramInt))
				paramInt = -1;
			else
				paramInt %= 18;

			var selection = int.Parse(wordList[0].Split(',')[1]);
            switch (selection)
            {
                case -1:
                    parameterName.Text = @"Two Factor 2nd LSD Sum / Unsolved Modules:";
                    return paramInt;

                case -2:
                    parameterName.Text = @"Solved Modules:";
                    return paramInt;

                case -3:
                    parameterName.Text = @"Strikes:";
                    return paramInt;

                case -4:
                    return _bombInfo.GetBatteryHolderCount();

                case -5:
                    return _bombInfo.CountUniquePorts();

                case -6:
                    return _bombInfo.GetPortCount();

                case -7:
                    return _bombInfo.GetOnIndicators().Count();

                case -8:
                    return _bombInfo.GetOffIndicators().Count();

                case -9:
                    return _bombInfo.GetIndicators().Count();

                case -10:
                    return _bombInfo.GetPortPlateCount();

                case -11:
                    return _bombInfo.GetSerialNumberNumbers().LastOrDefault();
                case -12:
                    return _bombInfo.GetSerialNumberNumbers().Sum();

                case -13:
                    return _bombInfo.GetBatteryCount();

                case -14:
                    return _bombInfo.GetSerialNumberNumbers().FirstOrDefault();

                case -15:
                    return Form1.Instance.GetBombMinutes;

                case -16:
                    parameterName.Text = @"Defuser Day of Week (Sunday = 0):";
                    return paramInt;

                case -17:
                    return _bombInfo.GetPortPlates().Count(x => x.Length == 0);

                case -18:
                    try
                    {
                        return "abcdefghijklmnopqrstuvwxyz".IndexOf(_bombInfo.GetSerialNumberLetters().First().ToString().ToLowerInvariant(), StringComparison.Ordinal);
                    }
                    catch
                    {
                        return 0;
                    }

                default:
                    return selection;
            }
        }
    }
}