using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class MemoryRuleSet : AbstractRuleSet
	{
		public MemoryRuleSet()
		{
			this.RulesDictionary = new Dictionary<int, List<Rule>>();
			for (int i = 0; i < MemoryComponent.NUM_STAGES; i++)
			{
				this.RulesDictionary.Add(i, new List<Rule>());
			}
		}

		public override void CacheStringValues()
		{
			foreach (KeyValuePair<int, List<Rule>> keyValuePair in this.RulesDictionary)
			{
				foreach (Rule rule in keyValuePair.Value)
				{
					rule.CacheStringValue();
				}
			}
		}

		public override string ToString()
		{
			string text = string.Empty;
			foreach (KeyValuePair<int, List<Rule>> keyValuePair in this.RulesDictionary)
			{
				text += string.Format("Stage {0}:\n", keyValuePair.Key + 1);
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					Rule rule = keyValuePair.Value[i];
					text += string.Format("If {0}, {1}.\n", rule.GetQueryString(), rule.GetSolutionString());
				}
				text += "\n";
			}
			return text;
		}

		public Dictionary<int, List<Rule>> RulesDictionary;
	}
}
