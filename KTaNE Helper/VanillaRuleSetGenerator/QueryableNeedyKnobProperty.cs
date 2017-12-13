using System.Collections.Generic;
using KTaNE_Helper.Modules;
using KTaNE_Helper.VanillaRuleSetGenerator.RuleSets;

namespace KTaNE_Helper.VanillaRuleSetGenerator
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
