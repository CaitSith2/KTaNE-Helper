﻿using System.Collections.Generic;
using KTaNE_Helper.Modules;

namespace KTaNE_Helper.VanillaRuleSetGenerator
{
	public class NeedyKnobSolutions
	{
		public static Solution UpPosition = new Solution
		{
			Text = "Up Position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 0)
		};

		public static Solution DownPosition = new Solution
		{
			Text = "Down Position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 1)
		};

		public static Solution LeftPosition = new Solution
		{
			Text = "Left Position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 2)
		};

		public static Solution RightPosition = new Solution
		{
			Text = "Right Position",
			SolutionMethod = ((BombComponent comp, Dictionary<string, object> args) => 3)
		};
	}
}