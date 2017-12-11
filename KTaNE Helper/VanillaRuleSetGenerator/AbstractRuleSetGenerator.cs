using System;
using System.Collections.Generic;
using Random = KTaNE_Helper.Helpers.MonoRandom;

namespace Assets.Scripts.Rules
{
	public abstract class AbstractRuleSetGenerator
	{
		public AbstractRuleSetGenerator()
		{
			this.solutionWeights = new Dictionary<Solution, float>();
			this.queryPropertyWeights = new Dictionary<QueryableProperty, float>();
		}

		public AbstractRuleSet GenerateRuleSet(int seed)
		{
			this.solutionWeights.Clear();
			this.queryPropertyWeights.Clear();
			this.rand = new Random(seed);
			return this.CreateRules(seed == DEFAULT_SEED);
		}

		protected abstract AbstractRuleSet CreateRules(bool useDefault);

		protected Solution SelectSolution(List<Solution> possibleSolutions)
		{
			float num = 0f;
			foreach (Solution key in possibleSolutions)
			{
				num += this.solutionWeights[key];
			}
			double num2 = this.rand.NextDouble() * (double)num;
			foreach (Solution solution in possibleSolutions)
			{
				if (num2 < (double)this.solutionWeights[solution])
				{
					Dictionary<Solution, float> dictionary;
					Solution key2;
					(dictionary = this.solutionWeights)[key2 = solution] = dictionary[key2] * 0.05f;
					return solution;
				}
				num2 -= (double)this.solutionWeights[solution];
			}
			return possibleSolutions[this.rand.Next(0, possibleSolutions.Count)];
		}

		protected QueryableProperty SelectQueryableProperty(List<QueryableProperty> possibleQueryableProperties)
		{
			float num = 0f;
			foreach (QueryableProperty key in possibleQueryableProperties)
			{
				if (!this.queryPropertyWeights.ContainsKey(key))
				{
					this.queryPropertyWeights.Add(key, 1f);
				}
				num += this.queryPropertyWeights[key];
			}
			double num2 = this.rand.NextDouble() * (double)num;
			foreach (QueryableProperty queryableProperty in possibleQueryableProperties)
			{
				if (num2 < (double)this.queryPropertyWeights[queryableProperty])
				{
					Dictionary<QueryableProperty, float> dictionary;
					QueryableProperty key2;
					(dictionary = this.queryPropertyWeights)[key2 = queryableProperty] = dictionary[key2] * 0.1f;
					return queryableProperty;
				}
				num2 -= (double)this.queryPropertyWeights[queryableProperty];
			}
			return possibleQueryableProperties[this.rand.Next(0, possibleQueryableProperties.Count)];
		}

		protected int GetNumRules()
		{
			double num = this.rand.NextDouble();
			if (num < 0.6)
			{
				return 3;
			}
			return 4;
		}

		protected int GetNumQueriesForRule()
		{
			double num = this.rand.NextDouble();
			if (num < 0.6)
			{
				return 1;
			}
			return 2;
		}

		protected Dictionary<Solution, float> solutionWeights;

		protected Dictionary<QueryableProperty, float> queryPropertyWeights;

		protected Random rand;
	    protected static int DEFAULT_SEED = 1;
	}
}
