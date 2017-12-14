using System.Collections.Generic;
using System.Linq;
using VanillaRuleGenerator.Rules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Modules
{
    public class WireSetComponent : BombComponent
    {
        private WireSetComponent()
        {
        }

        private static WireSetComponent _instance;
        public static WireSetComponent Instance => _instance ?? (_instance = new WireSetComponent());

        public int GetFirstIndexOfColor(WireColor color)
        {
            return wires.IndexOf(color);
        }

        public int GetLastIndexOfColor(WireColor color)
        {
            return wires.LastIndexOf(color);
        }

        public int GetColorCount(WireColor color)
        {
            return wires.Count(x => x == color);
        }

        public string GetSolution(string colors)
        {
            wires.Clear();
            foreach (var c in colors.ToUpperInvariant())
            {
                switch (c)
                {
                    case 'R':
                        wires.Add(WireColor.red);
                        break;
                    case 'Y':
                        wires.Add(WireColor.yellow);
                        break;
                    case 'B':
                        wires.Add(WireColor.blue);
                        break;
                    case 'W':
                        wires.Add(WireColor.white);
                        break;
                    case 'K':
                        wires.Add(WireColor.black );
                        break;
                    default:
                        return "";
                }
            }
            if (wires.Count < MIN_WIRES || wires.Count > MAX_WIRES)
                return "";

            var ruleSet = RuleManager.Instance.CurrentRules.WireRuleSet;
            try
            {
                var cut = ruleSet.ExecuteRuleList(this, ruleSet.RulesDictionary[wires.Count]);
                var ordinal = new[] {"First", "Second", "Third", "Fourth", "Fifth", "Sixth"};
                return $"Cut the {ordinal[cut]} wire.";
            }
            catch
            {
                return "Bomb edgework is required to caluclate the wire cutting results";
            }
        }

        public WireColor GetColorOfWireIndex(int index) => wires[index];

        public int WireCount => wires.Count;

        public List<WireColor> wires = new List<WireColor>();
        public static int MIN_WIRES = 3;
        public static int MAX_WIRES = 6;

    }
}