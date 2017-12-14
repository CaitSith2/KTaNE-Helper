using System.Collections.Generic;
using System.Text;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public abstract class AbstractRuleSet
	{
		public virtual void CacheStringValues()
		{
		}

		public int ExecuteRuleList(BombComponent bombComponent, List<Rule> ruleList)
		{
				if (this.sb == null)
				{
					this.sb = new StringBuilder();
				}
				this.sb.Remove(0, this.sb.Length);
				this.sb.AppendFormat("Getting solution index for component {0}\n", bombComponent.name);
			int num = 0;
			for (int i = 0; i < ruleList.Count; i++)
			{
				Rule rule = ruleList[i];
					this.sb.AppendFormat("Running rule {0}: \"{1}\"\n", i, rule.ToString());
				bool flag = false;
				for (int j = 0; j < rule.Queries.Count; j++)
				{
					if (!rule.Queries[j].Property.QueryFunc(bombComponent, rule.Queries[j].Args))
					{
							this.sb.AppendLine("Result: false. Rule failed.");
						flag = false;
						break;
					}
						this.sb.AppendLine("Result: true.");
					flag = true;
				}
				if (flag)
				{
					num = rule.Solution.SolutionMethod(bombComponent, rule.SolutionArgs);
						this.sb.AppendFormat("All queries passed. Solution method: {0}\n", rule.Solution.Text);
						this.sb.AppendFormat("Solution index is {0}\n", num);
					break;
				}
			}
		    //AbstractRuleSet.logger.Debug(this.sb);
			return num;
		}

		protected StringBuilder sb;

		protected int MAX_STRING_CAPACITY = 5000;
	}
}
