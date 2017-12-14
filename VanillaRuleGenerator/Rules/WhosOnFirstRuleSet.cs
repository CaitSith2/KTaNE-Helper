using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class WhosOnFirstRuleSet : AbstractRuleSet
	{
		public WhosOnFirstRuleSet()
		{
			this.displayWordToButtonIndexMap = new Dictionary<string, int>();
			this.precedenceMap = new Dictionary<string, List<string>>();
			this.precedenceMapStrings = new Dictionary<string, string>();
		}

		public override void CacheStringValues()
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.precedenceMap)
			{
				this.precedenceMapStrings.Add(keyValuePair.Key, string.Join(", ", keyValuePair.Value.ToArray()));
			}
		}

		/*public bool ButtonPushed(WhosOnFirstComponent component, int index)
		{
			WhosOnFirstRuleSet.Logger.DebugFormat("WoF button {0} pushed.", index);
			if (component.CurrentDisplayWordIndex < 0)
			{
				WhosOnFirstRuleSet.Logger.Debug("Button pressed during invalid phase! Strike!");
				return false;
			}
			if (!component.IsActive)
			{
				WhosOnFirstRuleSet.Logger.Debug("Component not active! Strike!");
				return false;
			}
			WhosOnFirstRuleSet.Logger.DebugFormat("Current DisplayWord: {0}", WhosOnFirstRuleSet.DisplayWords[component.CurrentDisplayWordIndex]);
			int num = this.displayWordToButtonIndexMap[WhosOnFirstRuleSet.DisplayWords[component.CurrentDisplayWordIndex]];
			KeypadButton keypadButton = component.Buttons[num];
			string text = keypadButton.GetText();
			WhosOnFirstRuleSet.Logger.DebugFormat("Button to check is {0}. Text: {1}\nPrecedence List is: {2}", num, text, this.precedenceMapStrings[text]);
			List<string> list = this.precedenceMap[text];
			foreach (string text2 in list)
			{
				for (int i = 0; i < component.Buttons.Length; i++)
				{
					if (text2.Equals(component.Buttons[i].GetText(), StringComparison.OrdinalIgnoreCase))
					{
						WhosOnFirstRuleSet.Logger.DebugFormat("Top precedence label is {0} (button index {1}). Button pushed was {2}. Result: {3}", new object[]
						{
							text2,
							i,
							index,
							(i != index) ? "Incorrect" : "Correct"
						});
						return i == index;
					}
				}
			}
			return false;
		}*/

		public override string ToString()
		{
			string text = "Display Word \"Look At\" Index:\n";
			foreach (KeyValuePair<string, int> keyValuePair in this.displayWordToButtonIndexMap)
			{
				text += string.Format("{0}: {1}, ", keyValuePair.Key, keyValuePair.Value);
			}
			text += "\n\nPrecedent Map:\n";
			foreach (KeyValuePair<string, List<string>> keyValuePair2 in this.precedenceMap)
			{
				text += string.Format("{0}: {1}\n", keyValuePair2.Key, string.Join(", ", keyValuePair2.Value.ToArray()));
			}
			return text;
		}

		public static readonly List<string> DisplayWords = new List<string>
		{
			"YES",
			"FIRST",
			"DISPLAY",
			"OKAY",
			"SAYS",
			"NOTHING",
			string.Empty,
			"BLANK",
			"NO",
			"LED",
			"LEAD",
			"READ",
			"RED",
			"REED",
			"LEED",
			"HOLD ON",
			"YOU",
			"YOU ARE",
			"YOUR",
			"YOU'RE",
			"UR",
			"THERE",
			"THEY'RE",
			"THEIR",
			"THEY ARE",
			"SEE",
			"C",
			"CEE"
		};

		public static readonly List<List<string>> KeypadWords = new List<List<string>>
		{
			new List<string>
			{
				"READY",
				"FIRST",
				"NO",
				"BLANK",
				"NOTHING",
				"YES",
				"WHAT",
				"UHHH",
				"LEFT",
				"RIGHT",
				"MIDDLE",
				"OKAY",
				"WAIT",
				"PRESS"
			},
			new List<string>
			{
				"YOU",
				"YOU ARE",
				"YOUR",
				"YOU'RE",
				"UR",
				"U",
				"UH HUH",
				"UH UH",
				"WHAT?",
				"DONE",
				"NEXT",
				"HOLD",
				"SURE",
				"LIKE"
			}
		};

		public Dictionary<string, int> displayWordToButtonIndexMap;

		public Dictionary<string, List<string>> precedenceMap;

		public Dictionary<string, string> precedenceMapStrings;
	}
}
