using System.Collections.Generic;
using VanillaRuleGenerator.Modules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
	public class WireSolutions
	{
		public static Solution WireIndex0 = new Solution
		{
			Text = "cut the first wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 0)
		};

		public static Solution WireIndex1 = new Solution
		{
			Text = "cut the second wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 1)
		};

		public static Solution WireIndex2 = new Solution
		{
			Text = "cut the third wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 2)
		};

		public static Solution WireIndex3 = new Solution
		{
			Text = "cut the fourth wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 3)
		};

		public static Solution WireIndex4 = new Solution
		{
			Text = "cut the fifth wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 4)
		};

		public static Solution WireLast = new Solution
		{
			Text = "cut the last wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => ((WireSetComponent)comp).wires.Count - 1)
		};

		public static Solution WireColorExactlyOne = new Solution
		{
			Text = "cut the {color} wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => ((WireSetComponent)comp).GetFirstIndexOfColor((WireColor)args["color"]))
		};

		public static Solution WireColorCutFirst = new Solution
		{
			Text = "cut the first {color} wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => ((WireSetComponent)comp).GetFirstIndexOfColor((WireColor)args["color"]))
		};

		public static Solution WireColorCutLast = new Solution
		{
			Text = "cut the last {color} wire",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => ((WireSetComponent)comp).GetLastIndexOfColor((WireColor)args["color"]))
		};
	}
}
