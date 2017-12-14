using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class MemoryRuleSetGenerator : AbstractRuleSetGenerator
	{
		protected override AbstractRuleSet CreateRules(bool useDefault)
		{
			MemoryRuleSet memoryRuleSet = new MemoryRuleSet();
			bool flag = true;
			for (int i = 0; i < MemoryComponent.NUM_STAGES; i++)
			{
				flag = !flag;
				for (int j = 1; j <= 4; j++)
				{
					Rule rule = new Rule();
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("digit", j);
					Query item = new Query
					{
						Property = QueryableMemoryProperty.IsDisplayDigit,
						Args = dictionary
					};
					rule.Queries.Add(item);
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2.Add("digit", this.rand.Next(1, 5));
					if (i > 0)
					{
						dictionary2.Add("stage", this.rand.Next(1, i + 1));
					}
					rule.Solution = base.SelectSolution(this.CreateSolutionsList(i, flag));
					rule.SolutionArgs = dictionary2;
					memoryRuleSet.RulesDictionary[i].Add(rule);
				}
			}
			return memoryRuleSet;
		}

		protected List<Solution> CreateSolutionsList(int stage, bool usePreviousIndex)
		{
			List<Solution> list = new List<Solution>();
			list.Add(MemorySolutions.ButtonIndex0);
			list.Add(MemorySolutions.ButtonIndex1);
			list.Add(MemorySolutions.ButtonIndex2);
			list.Add(MemorySolutions.ButtonIndex3);
			list.Add(MemorySolutions.ButtonLabelled);
			if (stage > 0)
			{
				if (usePreviousIndex)
				{
					list.Add(MemorySolutions.ButtonIndexPushedInPreviousStage);
				}
				else
				{
					list.Add(MemorySolutions.ButtonLabelPushedInPreviousStage);
				}
			}
			foreach (Solution key in list)
			{
				if (!this.solutionWeights.ContainsKey(key))
				{
					this.solutionWeights.Add(key, 1f);
				}
			}
			if (stage > 2)
			{
				this.solutionWeights[MemorySolutions.ButtonIndexPushedInPreviousStage] = 1f;
				this.solutionWeights[MemorySolutions.ButtonLabelPushedInPreviousStage] = 1f;
			}
			return list;
		}
	}
}
