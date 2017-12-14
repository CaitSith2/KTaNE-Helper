using System.Collections.Generic;
using System.Linq;

namespace VanillaRuleGenerator.Rules
{
	public class MorseCodeRuleSet : AbstractRuleSet
	{
		public MorseCodeRuleSet(Dictionary<int, string> wordDict)
		{
			this.WordDict = wordDict;
			this.ValidFrequencies = wordDict.Keys.ToList<int>();
			this.ValidFrequencies.Sort();
		}

		public Dictionary<int, string> WordDict { get; protected set; }

		public List<int> ValidFrequencies { get; protected set; }

		public override string ToString()
		{
			string text = "WordDict: \n";
			foreach (int num in this.ValidFrequencies)
			{
				text += string.Format("{0}: 3.{1} MHz\n", this.WordDict[num], num);
			}
			return text;
		}

	}
}
