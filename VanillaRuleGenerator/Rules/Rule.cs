using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class Rule
	{
		public void CacheStringValue()
		{
			this.cachedStringValue = string.Format("Query: {0}, Solution: {1}", this.GetQueryString(), this.GetSolutionString());
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.cachedStringValue))
			{
				//Rule.logger.Warn("Called ToString() without having previously cached this Rule's string! Could cause dropped frames during gameplay!");
				this.CacheStringValue();
			}
			return this.cachedStringValue;
		}

		public string GetQueryString()
		{
			string text = string.Empty;
			for (int i = 0; i < this.Queries.Count; i++)
			{
				text += this.Queries[i].ToString();
				if (i != this.Queries.Count - 1)
				{
					text += " and ";
				}
			}
			return text;
		}

		public string GetSolutionString()
		{
			return RuleUtil.SubArgs(this.Solution.Text, this.SolutionArgs);
		}

		public List<Query> Queries = new List<Query>();

		public Solution Solution;

		public Dictionary<string, object> SolutionArgs = new Dictionary<string, object>();

		protected string cachedStringValue;
	}
}
