using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class QueryableMemoryProperty : QueryableProperty
	{
		public static QueryableMemoryProperty IsDisplayDigit = new QueryableMemoryProperty
		{
			Name = "isDisplayDigit",
			Text = "the display is {digit}",
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				MemoryComponent memoryComponent = comp as MemoryComponent;
				if (memoryComponent != null)
				{
					int num = (int)args["digit"];
					if (memoryComponent.DisplayText.text.Equals(num.ToString()))
					{
						result = true;
					}
				}
				return result;
			}
		};
	}
}
