using System.Collections.Generic;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class QueryableProperty
	{
		public QuerySet QuerySet;

		public string Name;

		public string Text;

		public bool CompoundQueryOnly;

		public QueryFunc QueryFunc;

		public List<Solution> AdditionalSolutions;

		public static QueryableProperty IsSerialNumberOdd = new QueryableProperty
		{
			Name = "isSerialNumberOdd",
			Text = "the last digit of the serial number is odd",
			CompoundQueryOnly = true,
			QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => !SerialNumber.SerialNumberLastDigitEven())
		};

		public static QueryableProperty DoesSerialNumberStartWithLetter = new QueryableProperty
		{
			Name = "doesSerialNumberStartWithLetter",
			Text = "the serial number starts with a letter",
			CompoundQueryOnly = true,
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				string serialNumber = SerialNumber.Serial;
				return char.IsLetter(serialNumber[0]);
			}
		};

		public static QueryableProperty Otherwise = new QueryableProperty
		{
			Name = "otherwise",
			Text = string.Empty,
			QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => true)
		};

		public static QueryableProperty MoreThanXBatteries = new QueryableProperty
		{
			Name = "moreThanXBatteries",
			Text = "there {batteryCount:is;are} more than {batteryCount} {batteryCount:battery;batteries} on the bomb",
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
			    int totalBatteries = Batteries.TotalBatteries;
				return totalBatteries > (int)args["batteryCount"];
			}
		};

		public static QueryableProperty IndicatorXLit = new QueryableProperty
		{
			Name = "indicatorXLit",
			Text = "there is a lit indicator with label {label}",
			QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => Indicators.IsIndicatorLit((string)args["label"]))
		};
	}
}
