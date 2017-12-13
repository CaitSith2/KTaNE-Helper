using System;
using System.Text.RegularExpressions;

namespace KTaNE_Helper.Modules.Modded
{
    public class Stroop
    {
        public const int None = -1;
        public const int Red = 0;
        public const int Yellow = 1;
        public const int Green = 2;
        public const int Blue = 3;
        public const int Magenta = 4;
        public const int White = 5;

        public string GetAnswer(string stroopColors, string stroopWords)
        {
            const string colorLookup = "RYGBMW";
            var answer = "";
            var regex = new Regex("[BGMRWY]{8}");
            if (!regex.IsMatch(stroopColors) || !regex.IsMatch(stroopWords)) return String.Empty;
            var colors = new int[8];
            var words = new int[8];
            var colorMatchesWord = new bool[8];
            var totalMatches = 0;
            for (var i = 0; i < 8; i++)
            {
                colors[i] = colorLookup.IndexOf(stroopColors.Substring(i, 1), StringComparison.Ordinal);
                words[i] = colorLookup.IndexOf(stroopWords.Substring(i, 1), StringComparison.Ordinal);
                colorMatchesWord[i] = colors[i] == words[i];
                if (colorMatchesWord[i]) totalMatches++;
            }

            var colorsCount = new int[6];
            var wordsCount = new int[6];
            var colorAsWords = new int[6];

            for (var i = 0; i < 8; i++)
            {
                colorsCount[colors[i]]++;
                wordsCount[words[i]]++;
                if (colors[i] == words[i]) colorAsWords[colors[i]]++;
            }

            var condition = false;
            var lastWord = words[0];
            var lastColor = colors[0];
            switch (colors[7])
            {
                case Red:
                    if (wordsCount[Green] >= 3)
                    {
                        var j = 0;
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] == Green || words[i] == Green)
                                j++;
                            if (j < 3) continue;
                            answer = "Press Yes on " + (i + 1) + " (R1)";
                            break;
                        }
                    }
                    else if (colorsCount[Blue] == 1)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (words[i] != Magenta) continue;
                            answer = "Press No on " + (i + 1) + " (R2)";
                            break;
                        }
                    }
                    else
                    {
                        for (var i = 7; i >= 0; i--)
                        {
                            if (colors[i] != White && words[i] != White) continue;
                            answer = "Press Yes on " + (i + 1) + " (R3)";
                            break;
                        }
                    }
                    break;
                case Yellow:
                    for (var i = 0; i < 8 && !condition; i++)
                    {
                        condition = words[i] == Blue && colors[i] == Green;
                        if (!condition) continue;
                        for (var j = 0; j < 8; j++)
                        {
                            if (colors[j] != Green) continue;
                            answer = "Press Yes on " + (j + 1) + " (Y1)";
                            break;
                        }
                    }
                    for (var i = 0; i < 8 && !condition; i++)
                    {
                        condition = words[i] == White &&
                                    (colors[i] == White || colors[i] == Red);
                        if (!condition) continue;
                        var k = 0;
                        for (var j = 0; j < 8; j++)
                        {
                            if (words[j] != colors[j]) k++;
                            if (k < 2) continue;
                            answer = "Press Yes on " + (j + 1) + " (Y2)";
                            break;
                        }
                    }
                    if (!condition)
                        answer = "Press No on " +
                                 (colorsCount[Magenta] + wordsCount[Magenta] - colorAsWords[Magenta])
                                 + " (Y3)";
                    break;
                case Green:
                    for (var i = 1; i < 8 && !condition; i++)
                    {
                        if (words[i] == lastWord && colors[i] != lastColor)
                        {
                            answer = "Press No on 5 (G1)";
                            condition = true;
                        }
                        else
                        {
                            lastWord = words[i];
                            lastColor = colors[i];
                        }
                    }
                    if (!condition && wordsCount[Magenta] >= 3)
                    {
                        var j = 0;
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] == Yellow || words[i] == Yellow)
                                j++;
                            if (j < 1) continue;
                            answer = "Press No on " + (i + 1) + " (G2)";
                            break;
                        }
                    }
                    else if (!condition)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] != words[i]) continue;
                            answer = "Press Yes on " + (i + 1) + " (G3)";
                            break;
                        }
                    }
                    break;
                case Blue:
                    if (totalMatches <= 5)
                    {
                        for (var i = 0; i < 6 && !condition; i++)
                        {
                            if (colorMatchesWord[i]) continue;
                            answer = "Press Yes on " + (i + 1) + " (B1)";
                            condition = true;
                        }
                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8 && !condition; i++)
                        {
                            condition = (words[i] == Red && colors[i] == Yellow)
                                        || (words[i] == Yellow && colors[i] == White);
                            if (!condition) continue;
                            for (var j = 0; j < 8; j++)
                            {
                                if (words[j] != White || colors[j] != Red) continue;
                                answer = "Press No on " + (j + 1) + " (B2)";
                                break;
                            }
                        }
                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] == Green || words[i] == Green)
                                answer = "Press Yes on " + (i + 1) + " (B3)";
                        }

                    }
                    break;
                case Magenta:
                    for (var i = 1; i < 8 && !condition; i++)
                    {
                        if (words[i] != lastWord && colors[i] == lastColor)
                        {
                            answer = "Press Yes on 3 (M1)";
                            condition = true;
                        }
                        else
                        {
                            lastWord = words[i];
                            lastColor = colors[i];
                        }
                    }
                    if (wordsCount[Yellow] > colorsCount[Blue] && !condition)
                    {
                        condition = true;
                        for (var i = 0; i < 8; i++)
                            if (words[i] == Yellow)
                                answer = "Press No on " + (i + 1) + " (M2)";

                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] != words[6]) continue;
                            answer = "Press No on " + (i + 1) + " (M3)";
                            break;
                        }
                    }

                    break;
                case White:
                    if (colors[2] == words[3] || colors[2] == words[4])
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            if (colors[i] != Blue && words[i] != Blue) continue;
                            answer = "Press No on " + (i + 1) + " (W1)";
                            break;
                        }
                        condition = true;
                    }
                    if (!condition)
                    {
                        for (var i = 0; i < 8 && !condition; i++)
                        {
                            condition = words[i] == Yellow && colors[i] == Red;
                            if (!condition) continue;
                            for (var j = 0; j < 8; j++)
                            {
                                if (colors[j] == Blue)
                                    answer = "Press Yes on " + (j + 1) + " (W2)";
                            }

                        }
                    }
                    if (!condition)
                        answer = "Press No (W3)";
                    break;
                default:
                    return String.Empty;
            }
            return answer;
        }
    }
}