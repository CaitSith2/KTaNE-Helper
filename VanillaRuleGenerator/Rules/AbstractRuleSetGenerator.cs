using System.Collections.Generic;
using Random = VanillaRuleGenerator.Helpers.MonoRandom;

namespace VanillaRuleGenerator.Rules
{
	public abstract class AbstractRuleSetGenerator
	{
		public AbstractRuleSetGenerator()
		{
			solutionWeights = new Dictionary<Solution, float>();
			queryPropertyWeights = new Dictionary<QueryableProperty, float>();
		}

		public AbstractRuleSet GenerateRuleSet(int seed)
		{
			solutionWeights.Clear();
			queryPropertyWeights.Clear();
			rand = new Random(seed);
			return CreateRules(seed == RuleManager.DefaultSeed);
		}

		protected abstract AbstractRuleSet CreateRules(bool useDefault);

		protected Solution SelectSolution(List<Solution> possibleSolutions)
		{
			float num = 0f;
			foreach (Solution key in possibleSolutions)
			{
				num += solutionWeights[key];
			}
			double num2 = rand.NextDouble() * (double)num;
			foreach (Solution solution in possibleSolutions)
			{
				if (num2 < (double)solutionWeights[solution])
				{
					Dictionary<Solution, float> dictionary;
					Solution key2;
					(dictionary = solutionWeights)[key2 = solution] = dictionary[key2] * 0.05f;
					return solution;
				}
				num2 -= (double)solutionWeights[solution];
			}
			return possibleSolutions[rand.Next(0, possibleSolutions.Count)];
		}

		protected QueryableProperty SelectQueryableProperty(List<QueryableProperty> possibleQueryableProperties)
		{
			float num = 0f;
			foreach (QueryableProperty key in possibleQueryableProperties)
			{
				if (!queryPropertyWeights.ContainsKey(key))
				{
					queryPropertyWeights.Add(key, 1f);
				}
				num += queryPropertyWeights[key];
			}
			double num2 = rand.NextDouble() * (double)num;
			foreach (QueryableProperty queryableProperty in possibleQueryableProperties)
			{
				if (num2 < (double)queryPropertyWeights[queryableProperty])
				{
					Dictionary<QueryableProperty, float> dictionary;
					QueryableProperty key2;
					(dictionary = queryPropertyWeights)[key2 = queryableProperty] = dictionary[key2] * 0.1f;
					return queryableProperty;
				}
				num2 -= (double)queryPropertyWeights[queryableProperty];
			}
			return possibleQueryableProperties[rand.Next(0, possibleQueryableProperties.Count)];
		}

		protected int GetNumRules()
		{
			double num = rand.NextDouble();
			if (num < 0.6)
			{
				return 3;
			}
			return 4;
		}

		protected int GetNumQueriesForRule()
		{
			double num = rand.NextDouble();
			if (num < 0.6)
			{
				return 1;
			}
			return 2;
		}

		protected Dictionary<Solution, float> solutionWeights;

		protected Dictionary<QueryableProperty, float> queryPropertyWeights;

		protected Random rand;
	}
}
