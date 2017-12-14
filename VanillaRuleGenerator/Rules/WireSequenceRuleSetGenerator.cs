using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class WireSequenceRuleSetGenerator : AbstractRuleSetGenerator
	{
		public static int NumWiresPerColour
		{
			get
			{
				return WireSequenceRuleSetGenerator.NUM_PER_PAGE * (WireSequenceRuleSetGenerator.NUM_PAGES - WireSequenceRuleSetGenerator.BLANK_PAGE_COUNT);
			}
		}

		protected override AbstractRuleSet CreateRules(bool useDefault)
		{
			IList<int>[] array = new IList<int>[WireSequenceRuleSetGenerator.NumWiresPerColour];
			IList<int>[] array2 = new IList<int>[WireSequenceRuleSetGenerator.NumWiresPerColour];
			IList<int>[] array3 = new IList<int>[WireSequenceRuleSetGenerator.NumWiresPerColour];
			if (useDefault)
			{
				this.PopulateEmptySolution(array);
				this.PopulateEmptySolution(array2);
				this.PopulateEmptySolution(array3);
				array[0].Add(2);
				array[1].Add(1);
				array[2].Add(0);
				array[3].Add(0);
				array[3].Add(2);
				array[4].Add(1);
				array[5].Add(0);
				array[5].Add(2);
				array[6].Add(0);
				array[6].Add(1);
				array[6].Add(2);
				array[7].Add(0);
				array[7].Add(1);
				array[8].Add(1);
				array2[0].Add(1);
				array2[1].Add(0);
				array2[1].Add(2);
				array2[2].Add(1);
				array2[3].Add(0);
				array2[4].Add(1);
				array2[5].Add(1);
				array2[5].Add(2);
				array2[6].Add(2);
				array2[7].Add(0);
				array2[7].Add(2);
				array2[8].Add(0);
				array3[0].Add(0);
				array3[0].Add(1);
				array3[0].Add(2);
				array3[1].Add(0);
				array3[1].Add(2);
				array3[2].Add(1);
				array3[3].Add(0);
				array3[3].Add(2);
				array3[4].Add(1);
				array3[5].Add(1);
				array3[5].Add(2);
				array3[6].Add(0);
				array3[6].Add(1);
				array3[7].Add(2);
				array3[8].Add(2);
			}
			else
			{
				this.PopulateSolution(array);
				this.PopulateSolution(array2);
				this.PopulateSolution(array3);
			}
			return new WireSequenceRuleSet(array, array2, array3);
		}

		protected void PopulateSolution(IList<int>[] wiresToSnip)
		{
			for (int i = 0; i < wiresToSnip.Length; i++)
			{
				wiresToSnip[i] = new List<int>(WireSequenceRuleSetGenerator.NUM_COLOURS);
				for (int j = 0; j < WireSequenceRuleSetGenerator.NUM_COLOURS; j++)
				{
					if (this.rand.NextDouble() > 0.55)
					{
						wiresToSnip[i].Add(j);
					}
				}
			}
		}

		protected void PopulateEmptySolution(IList<int>[] wiresToSnip)
		{
			for (int i = 0; i < wiresToSnip.Length; i++)
			{
				wiresToSnip[i] = new List<int>(WireSequenceRuleSetGenerator.NumWiresPerColour);
			}
		}

		public static readonly int NUM_PAGES = 4;

		public static readonly int NUM_PER_PAGE = 3;

		public static readonly int NUM_COLOURS = 3;

		public static readonly int BLANK_PAGE_COUNT = 1;
	}
}
