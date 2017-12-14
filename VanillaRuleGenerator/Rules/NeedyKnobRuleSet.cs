using System.Collections.Generic;
using System.Text;
using VanillaRuleGenerator.Modules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
    public class NeedyKnobRuleSet : AbstractRuleSet
    {
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string a = string.Empty;
            foreach (Rule rule in this.Rules)
            {
                string text = rule.Solution.Text;
                if (a != text)
                {
                    a = text;
                    stringBuilder.Append(text).AppendLine(":");
                }
                foreach (Query query in rule.Queries)
                {
                    bool[] array = (bool[])query.Args[NeedyKnobRuleSet.LED_CONFIG_ARG_KEY];
                    for (int i = 0; i < NeedyKnobRuleSetGenerator.LED_ROWS; i++)
                    {
                        for (int j = 0; j < NeedyKnobRuleSetGenerator.LED_COLS; j++)
                        {
                            if (array[NeedyKnobRuleSetGenerator.LED_COLS * i + j])
                            {
                                stringBuilder.Append('X');
                            }
                            else
                            {
                                stringBuilder.Append('O');
                            }
                        }
                        stringBuilder.AppendLine();
                    }
                    stringBuilder.AppendLine();
                }
            }
            return stringBuilder.ToString();
        }

        public Direction GetSolution(NeedyKnobComponent component)
        {
            return (Direction)base.ExecuteRuleList(component, this.Rules);
        }

        public static bool CompareLEDConfigs(bool[] config1, bool[] config2)
        {
            bool result = true;
            for (int i = 0; i < config1.Length; i++)
            {
                if (config1[i] != config2[i])
                {
                    result = false;
                }
            }
            return result;
        }

        public static readonly string LED_CONFIG_ARG_KEY = "LEDConfig";

        public List<Rule> Rules;

        public List<bool[]> LEDConfigurations;
    }
}
