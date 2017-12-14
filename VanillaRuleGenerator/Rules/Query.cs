using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class Query
	{
		public override string ToString()
		{
			return RuleUtil.SubArgs(this.Property.Text, this.Args);
		}

		public QueryableProperty Property;

		public Dictionary<string, object> Args = new Dictionary<string, object>();
	}
}
