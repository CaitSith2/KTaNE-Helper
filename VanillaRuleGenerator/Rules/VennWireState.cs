namespace VanillaRuleGenerator.Rules
{
	public struct VennWireState
	{
		public VennWireState(bool red, bool blue, bool symbol, bool led)
		{
			this.HasRed = red;
			this.HasBlue = blue;
			this.HasSymbol = symbol;
			this.HasLED = led;
		}

		public override string ToString()
		{
			return string.Format("Red:{0} Blue:{1} Symbol:{2} LED:{3}", new object[]
			{
				this.HasRed,
				this.HasBlue,
				this.HasSymbol,
				this.HasLED
			});
		}

		public bool HasRed;

		public bool HasBlue;

		public bool HasSymbol;

		public bool HasLED;
	}
}
