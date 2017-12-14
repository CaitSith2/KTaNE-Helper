using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class ButtonRuleSet : AbstractRuleSet
	{
		public ButtonRuleSet()
		{
			this.RuleList = new List<Rule>();
			this.HoldRuleList = new List<Rule>();
		}

		public bool ButtonReleased(BombComponent component)
		{
			int num = base.ExecuteRuleList(component, this.RuleList);
			ButtonComponent buttonComponent = (ButtonComponent)component;
			int num2 = base.ExecuteRuleList(component, this.HoldRuleList);
			if (!buttonComponent.IsHolding)
			{
				num2 = 0;
			}
			return num == 0 && num2 == 0;
		}

		public override void CacheStringValues()
		{
			foreach (Rule rule in this.RuleList)
			{
				rule.CacheStringValue();
			}
			foreach (Rule rule2 in this.HoldRuleList)
			{
				rule2.CacheStringValue();
			}
		}

		public override string ToString()
		{
			string text = string.Empty;
			text += "Initial rules\n";
			foreach (Rule rule in this.RuleList)
			{
				text += string.Format("If {0}, {1}.\n", rule.GetQueryString(), rule.GetSolutionString());
			}
			text += "\nOn Hold Rules\n";
			foreach (Rule rule2 in this.HoldRuleList)
			{
				text += string.Format("If {0}, {1}.\n", rule2.GetQueryString(), rule2.GetSolutionString());
			}
			return text;
		}

		public List<Rule> RuleList;

		public List<Rule> HoldRuleList;
	}
}
