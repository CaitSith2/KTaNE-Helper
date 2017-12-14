using System.Collections.Generic;
using VanillaRuleGenerator.Extensions;

namespace VanillaRuleGenerator.Rules
{
	public class WhosOnFirstRuleSetGenerator : AbstractRuleSetGenerator
	{
		protected override AbstractRuleSet CreateRules(bool useDefault)
		{
			WhosOnFirstRuleSet whosOnFirstRuleSet = new WhosOnFirstRuleSet();
			foreach (string key in WhosOnFirstRuleSet.DisplayWords)
			{
				whosOnFirstRuleSet.displayWordToButtonIndexMap.Add(key, this.rand.Next(0, WhosOnFirstRuleSetGenerator.NUM_BUTTONS));
			}
			foreach (List<string> list in WhosOnFirstRuleSet.KeypadWords)
			{
				foreach (string key2 in list)
				{
					List<string> list2 = new List<string>(list);
					list2.Shuffle(this.rand);
					whosOnFirstRuleSet.precedenceMap.Add(key2, list2);
				}
			}
			return whosOnFirstRuleSet;
		}

		public static readonly int NUM_BUTTONS = 6;
	}
}
