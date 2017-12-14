using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class WireRuleSet : AbstractRuleSet
	{
		public WireRuleSet()
		{
			this.RulesDictionary = new Dictionary<int, List<Rule>>();
			this.RulesDictionary.Add(3, new List<Rule>());
			this.RulesDictionary.Add(4, new List<Rule>());
			this.RulesDictionary.Add(5, new List<Rule>());
			this.RulesDictionary.Add(6, new List<Rule>());
		}

		public override void CacheStringValues()
		{
			foreach (List<Rule> list in this.RulesDictionary.Values)
			{
				foreach (Rule rule in list)
				{
					rule.CacheStringValue();
				}
			}
		}

		public int GetSolutionIndex(WireSetComponent wireComponent)
		{
			List<Rule> ruleList = this.RulesDictionary[wireComponent.WireCount];
			return base.ExecuteRuleList(wireComponent, ruleList);
		}

		public override string ToString()
		{
			string text = string.Empty;
			foreach (KeyValuePair<int, List<Rule>> keyValuePair in this.RulesDictionary)
			{
				text += string.Format("{0} wires:\n", keyValuePair.Key);
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					Rule rule = keyValuePair.Value[i];
					if (i != keyValuePair.Value.Count - 1)
					{
						text += string.Format("If {0}, {1}.\n", rule.GetQueryString(), rule.GetSolutionString());
					}
					else
					{
						text = text + "Otherwise, " + rule.GetSolutionString() + ".\n";
					}
				}
				text += "\n";
			}
			return text;
		}

		public Dictionary<int, List<Rule>> RulesDictionary;
	}
}
