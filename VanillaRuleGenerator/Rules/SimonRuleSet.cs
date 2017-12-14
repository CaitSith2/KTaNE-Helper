using System.Collections.Generic;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
    public class SimonRuleSet : AbstractRuleSet
    {
        public int[] GetSolutionMap(string serialNumber, int strikeCount)
        {
            if (strikeCount > 2)
            {
                strikeCount = 2;
            }
            SimonColor[] array = this.RuleList[(!SerialNumber.SerialNumberContainsVowel()) ? "OTHERWISE" : "HASVOWEL"][strikeCount];
            return new int[]
            {
                (int)array[0],
                (int)array[1],
                (int)array[2],
                (int)array[3]
            };
        }

        public override string ToString()
        {
            string text = string.Empty;
            foreach (KeyValuePair<string, List<SimonColor[]>> keyValuePair in this.RuleList)
            {
                text += string.Format("{0}:\n", keyValuePair.Key);
                for (int i = 0; i < keyValuePair.Value.Count; i++)
                {
                    text += string.Format("{0} Strikes: ", i);
                    foreach (SimonColor simonColor in keyValuePair.Value[i])
                    {
                        text = text + simonColor + ", ";
                    }
                    text += "\n";
                }
            }
            return text;
        }

        public Dictionary<string, List<SimonColor[]>> RuleList;

        public const string HAS_VOWEL_STRING = "HASVOWEL";

        public const string OTHERWISE_STRING = "OTHERWISE";
    }
}
