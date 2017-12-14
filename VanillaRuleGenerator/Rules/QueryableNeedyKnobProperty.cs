using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class QueryableNeedyKnobProperty : QueryableProperty
	{

		public static QueryableNeedyKnobProperty MatchesLEDConfig = new QueryableNeedyKnobProperty
		{
			Name = "matchesLEDConfig",
			Text = "{LEDConfig}",
			CompoundQueryOnly = false,
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool[] config = (bool[])args[NeedyKnobRuleSet.LED_CONFIG_ARG_KEY];
				bool[] ledconfiguration = ((NeedyKnobComponent)comp).LEDConfiguration;
				return NeedyKnobRuleSet.CompareLEDConfigs(config, ledconfiguration);
			}
		};
	}
}
