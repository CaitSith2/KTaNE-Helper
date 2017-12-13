using System;
using System.Collections.Generic;

namespace Assets.Scripts.Rules
{
	public class KeypadRuleSet : AbstractRuleSet
	{
		public KeypadRuleSet()
		{
			this.PrecedenceLists = new List<List<string>>();
			this.FileNames = new List<List<string>>();
		}

		public override string ToString()
		{
			string text = string.Empty;
			foreach (List<string> list in this.PrecedenceLists)
			{
				text = text + string.Join(",", list.ToArray()) + "\n";
			}
			return text;
		}

		public List<List<string>> PrecedenceLists;

		public List<List<string>> FileNames;
	}
}
