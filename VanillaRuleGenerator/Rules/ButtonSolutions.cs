using System.Collections.Generic;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class ButtonSolutions
	{
		public static Solution ReleaseOnTimerText(string searchText)
		{
			return new Solution
			{
				Text = string.Format("release when the countdown timer has a {0} in any position.", searchText),
				SolutionMethod = (BombComponent comp, Dictionary<string, object> args) => int.Parse(searchText)
			};
		}

		public static Solution Press = new Solution
		{
			Text = "press and immediately release the button",
		    SolutionMethod = (BombComponent comp, Dictionary<string, object> args) => 0
        };

		public static Solution Hold = new Solution
		{
			Text = "hold the button and refer to \"Releasing a Held Button\"",
		    SolutionMethod = (BombComponent comp, Dictionary<string, object> args) => 1
        };

	    public static Solution[] PressSolutions = new[]
	    {
	        Press, Hold, ButtonRuleGenerator.TapWhenSecondsMatch
	    };

	    public static Solution[] HoldSolutions = new[]
	    {
            ReleaseOnTimerText("0"),ReleaseOnTimerText("1"),ReleaseOnTimerText("2"),ReleaseOnTimerText("3"),ReleaseOnTimerText("4"),
            ReleaseOnTimerText("5"),ReleaseOnTimerText("6"),ReleaseOnTimerText("7"),ReleaseOnTimerText("8"),ReleaseOnTimerText("9"),
	        ButtonRuleGenerator.ReleaseWhenSecondsDigitsAddsToSeven,
	        ButtonRuleGenerator.ReleaseWhenSecondsDigitsAddsToThreeOrThirteen,
	        ButtonRuleGenerator.ReleaseWhenSecondsDigitsAddsToFive,
	        ButtonRuleGenerator.ReleaseWhenSecondsIsMultipleOfSeven,
	        ButtonRuleGenerator.ReleaseWhenSecondsPrimeOrZero,
	        ButtonRuleGenerator.ReleaseOneSecondAfterSecondsAddToMultipleOfFour,
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(0),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(1),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(2),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(3),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(4),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(5),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(6),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(7),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(8),
	        ButtonRuleGenerator.ReleaseWhenLeastSignificantSecondIs(9),
            ButtonRuleGenerator.ReleaseAtAnyTime,

        };
	}
}
