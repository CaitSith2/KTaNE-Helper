using System.Collections.Generic;
using VanillaRuleGenerator.Modules;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
	public class QueryableButtonProperty : QueryableProperty
	{
		public static QueryableButtonProperty IsButtonColor = new QueryableButtonProperty
		{
			Name = "isButtonColor",
			Text = "the button is {color}",
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				ButtonComponent buttonComponent = comp as ButtonComponent;
				if (buttonComponent != null)
				{
					ButtonColor buttonColor = (ButtonColor)args["color"];
					if (buttonComponent.ButtonColor == buttonColor)
					{
						result = true;
					}
				}
				return result;
			}
		};

		public static QueryableButtonProperty IsButtonInstruction = new QueryableButtonProperty
		{
			Name = "isButtonColor",
			Text = "the button says \"{instruction}\"",
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				ButtonComponent buttonComponent = comp as ButtonComponent;
				if (buttonComponent != null)
				{
					ButtonInstruction buttonInstruction = (ButtonInstruction)args["instruction"];
					if (buttonComponent.ButtonInstruction == buttonInstruction)
					{
						result = true;
					}
				}
				return result;
			}
		};

		public static QueryableButtonProperty IsIndicatorColor = new QueryableButtonProperty
		{
			Name = "isButtonColor",
			Text = "{color} strip:",
			QueryFunc = delegate(BombComponent comp, Dictionary<string, object> args)
			{
				bool result = false;
				ButtonComponent buttonComponent = comp as ButtonComponent;
				if (buttonComponent != null)
				{
					BigButtonLEDColor bigButtonLEDColor = (BigButtonLEDColor)args["color"];
					if (buttonComponent.IndicatorColor == bigButtonLEDColor)
					{
						result = true;
					}
				}
				return result;
			}
		};

		public static QueryableButtonProperty IndicatorOtherwise = new QueryableButtonProperty
		{
			Name = "otherwiseindicator",
			Text = "Any other color strip:",
			QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => true)
		};

		public static QueryableButtonProperty ButtonOtherwise = new QueryableButtonProperty
		{
			Name = "otherwise",
			Text = "none of the above apply",
			QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => true)
		};
	}
}
