using System;
using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
    public class NeedyKnobRuleSetGenerator : AbstractRuleSetGenerator
    {
        protected override AbstractRuleSet CreateRules(bool useDefault)
        {
            NeedyKnobRuleSet needyKnobRuleSet = new NeedyKnobRuleSet();
            Solution[] array = new Solution[]
            {
                NeedyKnobSolutions.UpPosition,
                NeedyKnobSolutions.DownPosition,
                NeedyKnobSolutions.LeftPosition,
                NeedyKnobSolutions.RightPosition
            };
            int num = array.Length * NeedyKnobRuleSetGenerator.NUM_RULES_PER_SOLUTION;
            needyKnobRuleSet.Rules = new List<Rule>(num);
            needyKnobRuleSet.LEDConfigurations = new List<bool[]>(num);
            List<bool[]> list = this.GeneratePossibilities(num);
            foreach (Solution solution in array)
            {
                for (int j = 0; j < NeedyKnobRuleSetGenerator.NUM_RULES_PER_SOLUTION; j++)
                {
                    Rule rule = new Rule();
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    bool[] array3 = list[this.rand.Next(list.Count)];
                    list.Remove(array3);
                    dictionary[NeedyKnobRuleSet.LED_CONFIG_ARG_KEY] = array3;
                    Query item = new Query
                    {
                        Property = QueryableNeedyKnobProperty.MatchesLEDConfig,
                        Args = dictionary
                    };
                    rule.Queries.Add(item);
                    rule.Solution = solution;
                    needyKnobRuleSet.Rules.Add(rule);
                    needyKnobRuleSet.LEDConfigurations.Add(array3);
                }
            }
            return needyKnobRuleSet;
        }

        protected List<bool[]> GeneratePossibilities(int numRules)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < (int)Math.Pow(2f, (float)NeedyKnobRuleSetGenerator.LED_COLS); i++)
            {
                list.Add(i);
            }
            List<bool[]> list2 = new List<bool[]>();
            do
            {
                int num = list[this.rand.Next(list.Count)];
                list.Remove(num);
                int num2 = list[this.rand.Next(list.Count)];
                list.Remove(num2);
                int num3 = list[this.rand.Next(list.Count)];
                list.Remove(num3);
                int a = num << NeedyKnobRuleSetGenerator.LED_COLS | num2;
                int a2 = num << NeedyKnobRuleSetGenerator.LED_COLS | num3;
                int num4 = list[this.rand.Next(list.Count)];
                list.Remove(num4);
                int num5 = list[this.rand.Next(list.Count)];
                list.Remove(num5);
                int num6 = list[this.rand.Next(list.Count)];
                list.Remove(num6);
                int a3 = num5 << NeedyKnobRuleSetGenerator.LED_COLS | num4;
                int a4 = num6 << NeedyKnobRuleSetGenerator.LED_COLS | num4;
                list2.Add(intToBoolArray(a, NeedyKnobRuleSetGenerator.LED_COLS * NeedyKnobRuleSetGenerator.LED_ROWS));
                list2.Add(intToBoolArray(a2, NeedyKnobRuleSetGenerator.LED_COLS * NeedyKnobRuleSetGenerator.LED_ROWS));
                list2.Add(intToBoolArray(a3, NeedyKnobRuleSetGenerator.LED_COLS * NeedyKnobRuleSetGenerator.LED_ROWS));
                list2.Add(intToBoolArray(a4, NeedyKnobRuleSetGenerator.LED_COLS * NeedyKnobRuleSetGenerator.LED_ROWS));
            }
            while (list2.Count < numRules);
            return list2;
        }

        public static bool[] intToBoolArray(int a, int arraySize)
        {
            bool[] array = new bool[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = ((a & 1) == 1);
                a >>= 1;
            }
            return array;
        }

        public static readonly int NUM_RULES_PER_SOLUTION = 2;

        public static readonly int LED_ROWS = 2;

        public static readonly int LED_COLS = 6;

        public static readonly int LED_LIT = 4;
    }
}
