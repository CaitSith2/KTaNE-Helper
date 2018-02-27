using System.Linq;
using VanillaRuleGenerator.Edgework;
using static VanillaRuleGenerator.Edgework.Indicators;
using static VanillaRuleGenerator.Edgework.PortPlate;
using static VanillaRuleGenerator.Edgework.SerialNumber;

namespace KTaNE_Helper.Modules.Modded
{
	public static class Laundry
	{
		public static string GetLaundrySolution(string input, int moduleCount)
		{
			if (LitIndicators.Contains("BOB") && Batteries.Holders.Sum() == 4 &&
				Batteries.Holders.Length == 2)
			{
				return @"Praise our one true lord and savior BOB, Bestower of Bleach";
			}

			if (!int.TryParse(input, out int x))
			{
				return "";
			}

			var item = moduleCount - x + UnlitIndicators.Length + LitIndicators.Length;
			var material = PortCount(PortTypes.ALL) + x - Batteries.Holders.Length;
			var color = LastSerialDigit + Batteries.TotalBatteries;

			while (item < 0)
			{
				item += 6;
			}
			while (material < 0)
			{
				material += 6;
			}
			item %= 6;
			material %= 6;
			color %= 6;

			var materialNames = new[] { "POLYESTER", "COTTON", "WOOL", "NYLON", "CORDUROY", "LEATHER" };
			bool serialMatchesMaterial = SerialNumberLetters.Any(letter => materialNames[material].Contains(letter));

			var washing = new[] { 6, 9, 2, 4, 5, 4 };
			var materialSpecial = new[] { 6, 8, 10, 11, 7, 5 };
			var drying = new[] { 3, 3, 0, 10, 1, 2 };
			var colorSpecial = new[] { 4, 11, 9, 12, 2, 2 };
			var ironing = new[] { 3, 5, 0, 4, 3, 2 };
			var itemSpecial = new[] { 0, 5, 10, 10, 1, 8 };

			var specialtInstructionsText = "Item";
			var specialInstructions = itemSpecial[item];
			if (color == 4)
			{
				specialInstructions = 2;
				specialtInstructionsText = "Color==4";
			}
			else if (item == 0 || material == 4)
			{
				specialInstructions = materialSpecial[material];
				specialtInstructionsText = "Material";
			}
			else if (serialMatchesMaterial)
			{
				specialInstructions = colorSpecial[color];
				specialtInstructionsText = "Color";
			}

			var washingInstructions = color == 3 ? 4 : washing[material];
			var washingOverride = color == 3 ? "Washing" : "";
			var dryingInstructions = material == 2 ? 3 : drying[color];
			var dryingOverride = material == 2 ? "Drying" : "";
			var ironingInstructions = ironing[item];
			var overrideText = washingOverride == "" && dryingOverride == "" ? "" : " Override:";
			var overrideSeperator = washingOverride != "" && dryingOverride != "" ? "," : "";

			string result = $@"set all {washingInstructions},{dryingInstructions},{ironingInstructions},{specialInstructions}";
			result += $@" -- I:{item}, M:{material}, C:{color}, Special:{specialtInstructionsText}{overrideText}{washingOverride}{overrideSeperator}{dryingOverride}";
			return result;
		}
	}
}