using System.Collections.Generic;
using VanillaRuleGenerator.Modules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
	public class QueryableWireProperty : QueryableProperty
	{
		public bool UsesColor;

		public bool ColorAvailableForSolution;

		public int WiresInvolvedInQuery;
		public static QueryableWireProperty IsExactlyOneColorWire = new QueryableWireProperty
		{
			Name = "isExactlyOneColorWire",
			Text = "there is exactly one {color} wire",
			UsesColor = true,
			ColorAvailableForSolution = true,
			WiresInvolvedInQuery = 1,
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				WireSetComponent wireSetComponent = comp as WireSetComponent;
				if (wireSetComponent != null)
				{
					WireColor color = (WireColor)args["color"];
					if (wireSetComponent.GetColorCount(color) == 1)
					{
						result = true;
					}
				}
				return result;
			},
			AdditionalSolutions = new List<Solution>
			{
				WireSolutions.WireColorExactlyOne
			}
		};

		public static QueryableWireProperty MoreThanOneColorWire = new QueryableWireProperty
		{
			Name = "isMoreThanOneColorWire",
			Text = "there is more than one {color} wire",
			UsesColor = true,
			ColorAvailableForSolution = true,
			WiresInvolvedInQuery = 2,
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				WireSetComponent wireSetComponent = comp as WireSetComponent;
				if (wireSetComponent != null)
				{
					WireColor color = (WireColor)args["color"];
					if (wireSetComponent.GetColorCount(color) >= 2)
					{
						result = true;
					}
				}
				return result;
			},
			AdditionalSolutions = new List<Solution>
			{
				WireSolutions.WireColorCutFirst,
				WireSolutions.WireColorCutLast
			}
		};

		public static QueryableWireProperty IsExactlyZeroColorWire = new QueryableWireProperty
		{
			Name = "isExactlyZeroColorWire",
			Text = "there are no {color} wires",
			UsesColor = true,
			ColorAvailableForSolution = false,
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				WireSetComponent wireSetComponent = comp as WireSetComponent;
				if (wireSetComponent != null)
				{
					WireColor color = (WireColor)args["color"];
					if (wireSetComponent.GetColorCount(color) == 0)
					{
						result = true;
					}
				}
				return result;
			}
		};

		public static QueryableWireProperty LastWireIsColor = new QueryableWireProperty
		{
			Name = "isLastWireColor",
			Text = "the last wire is {color}",
			UsesColor = true,
			ColorAvailableForSolution = true,
			WiresInvolvedInQuery = 1,
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				WireSetComponent wireSetComponent = comp as WireSetComponent;
				if (wireSetComponent != null)
				{
					WireColor wireColor = (WireColor)args["color"];
					if (wireSetComponent.GetColorOfWireIndex(wireSetComponent.WireCount - 1) == wireColor)
					{
						result = true;
					}
				}
				return result;
			}
		};
	}
}
