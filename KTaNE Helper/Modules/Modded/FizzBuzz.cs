using System.Linq;
using System.Text.RegularExpressions;
using VanillaRuleGenerator.Edgework;
using static VanillaRuleGenerator.Edgework.Batteries;
using static VanillaRuleGenerator.Edgework.PortPlate;
using static VanillaRuleGenerator.Edgework.SerialNumber;

namespace KTaNE_Helper.Modules.Modded
{
	public static class FizzBuzz
	{
		public static string ComputeFizzBuzz(string text, int strikes)
		{
			var match = Regex.Match(text, "^([0-9]{7})((?:R|G|B|Y|W)?)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
			if (!match.Success) return "";

			var colorText = new[] { "Red", "Green", "Blue", "Yellow", "White" };
			var digits = match.Groups[1].Value.ToArray().Select(x => int.Parse(x.ToString())).ToArray();
			int colorIndex = string.IsNullOrEmpty(match.Groups[2].Value) 
				? -1
				: colorText.Select(x => x.Remove(1)).ToList().IndexOf(match.Groups[2].Value.ToUpperInvariant());

			var fizzBuzzTable = new[]
			{
				new[] {7, 3, 2, 4, 5},
				new[] {3, 4, 9, 2, 8},
				new[] {4, 5, 8, 8, 2},
				new[] {2, 3, 7, 9, 1},
				new[] {6, 6, 1, 2, 8},
				new[] {1, 2, 2, 5, 3},
				new[] {3, 1, 8, 3, 4}
			};
			var colors = new[] { 0, 0, 0, 0, 0 };
			for (var i = 0; i < 5; i++)
			{
				if (Holders.Length >= 3)
					colors[i] += fizzBuzzTable[0][i];
				if (IsPortPresent(PortTypes.Parallel) && IsPortPresent(PortTypes.Serial))
					colors[i] += fizzBuzzTable[1][i];
				if (SerialNumberDigits.Length == 3 && SerialNumberLetters.Length == 3)
					colors[i] += fizzBuzzTable[2][i];
				if (IsPortPresent(PortTypes.DVI) && IsPortPresent(PortTypes.StereoRCA))
					colors[i] += fizzBuzzTable[3][i];
				if (strikes >= 2)
					colors[i] += fizzBuzzTable[4][i];
				if (TotalBatteries >= 5)
					colors[i] += fizzBuzzTable[5][i];
				if (colors[i] == 0)
					colors[i] = fizzBuzzTable[6][i];
				colors[i] %= 10;
			}

			string result = "";
			for (var i = (colorIndex < 0 ? 0 : colorIndex); i < (colorIndex < 0 ? 5 : (colorIndex + 1)); i++)
			{
				int number = 0;
				foreach (var digit in digits)
				{
					int x = digit;
					x += colors[i];
					x %= 10;
					number *= 10;
					number += x;
				}
				result += colorText[i] + @":";
				var fizz = number % 3 == 0;
				var buzz = number % 5 == 0;
				if (!fizz && !buzz)
					result += "Number";
				if (fizz)
					result += "Fizz";
				if (buzz)
					result += "Buzz";
				if (i != 4 && colorIndex < 0)
					result += " - ";
			}
			return result;
		}
	}
}