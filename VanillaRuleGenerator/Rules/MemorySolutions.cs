using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class MemorySolutions
	{
		public static Solution ButtonIndex0 = new Solution
		{
			Text = "press the button in the first position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 0)
		};

		public static Solution ButtonIndex1 = new Solution
		{
			Text = "press the button in the second position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 1)
		};

		public static Solution ButtonIndex2 = new Solution
		{
			Text = "press the button in the third position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 2)
		};

		public static Solution ButtonIndex3 = new Solution
		{
			Text = "press the button in the fourth position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 3)
		};

		public static Solution ButtonLabelled = new Solution
		{
			Text = "press the button labeled \"{digit}\"",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => (4 + (int)args["digit"] - 1))
		};

		public static Solution ButtonIndexPushedInPreviousStage = new Solution
		{
			Text = "press the button in the same position as you pressed in stage {stage}",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => (8 + (int)args["stage"] - 1))
		};

		public static Solution ButtonLabelPushedInPreviousStage = new Solution
		{
			Text = "press the button with the same label you pressed in stage {stage}",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => (12 + (int)args["stage"] - 1))
		};
	}
}
