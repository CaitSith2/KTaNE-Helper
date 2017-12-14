using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public delegate bool QueryFunc(BombComponent comp, Dictionary<string, object> args);
}
