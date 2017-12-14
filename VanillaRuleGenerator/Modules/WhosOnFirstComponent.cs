using System.Linq;
using VanillaRuleGenerator.Rules;

namespace VanillaRuleGenerator.Modules
{
    public class WhosOnFirstComponent : BombComponent
    {
        private WhosOnFirstComponent()
        {
        }

        private static WhosOnFirstComponent _instance;
        public static WhosOnFirstComponent Instance => _instance ?? (_instance = new WhosOnFirstComponent());


        public string[] WhosOnFirstStep1WordList => WhosOnFirstRuleSet.DisplayWords.OrderBy(x => x).ToArray();

        public string[] WhosOnFirstStep2WordList => WhosOnFirstRuleSet.KeypadWords[0].Concat(WhosOnFirstRuleSet.KeypadWords[1]).OrderBy(x => x).ToArray();

        public int WhosOnFistStep1Index(string word)
        {
            var ruleSet = RuleManager.Instance.CurrentRules.WhosOnFirstRuleSet;
            return ruleSet.displayWordToButtonIndexMap.ContainsKey(word) ? ruleSet.displayWordToButtonIndexMap[word] : -1;
        }

        public string[] Step2WordOrder(string word)
        {
            var ruleSet = RuleManager.Instance.CurrentRules.WhosOnFirstRuleSet;
            return ruleSet.precedenceMap.ContainsKey(word) ? ruleSet.precedenceMap[word].ToArray() : new [] {"","","","","","","","","","","","","","" };
        }
    }
}