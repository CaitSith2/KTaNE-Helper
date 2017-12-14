namespace VanillaRuleGenerator.Rules
{
	public class BombRules
	{
		public void CacheStringValues()
		{
			if (!this.hasCachedStringValues)
			{
				this.WireRuleSet.CacheStringValues();
				this.WhosOnFirstRuleSet.CacheStringValues();
				this.KeypadRuleSet.CacheStringValues();
				this.MemoryRuleSet.CacheStringValues();
				this.NeedyKnobRuleSet.CacheStringValues();
				this.ButtonRuleSet.CacheStringValues();
				this.WireSequenceRuleSet.CacheStringValues();
				this.PasswordRuleSet.CacheStringValues();
				this.MorseCodeRuleSet.CacheStringValues();
				this.VennWireRuleSet.CacheStringValues();
				this.MazeRuleSet.CacheStringValues();
				this.SimonRuleSet.CacheStringValues();
				this.hasCachedStringValues = true;
			}
		}

		public void PrintRules()
		{
			/*BombRules.logger.DebugFormat("BombRules: {0}", this.ManualMetaData.ToString());
			BombRules.logger.Debug("WireSet Rules:\n" + this.WireRuleSet.ToString());
			BombRules.logger.Debug("Who's On First Rules:\n" + this.WhosOnFirstRuleSet.ToString());
			BombRules.logger.Debug("Keypad Rules:\n" + this.KeypadRuleSet.ToString());
			BombRules.logger.Debug("Memory Rules:\n" + this.MemoryRuleSet.ToString());
			BombRules.logger.Debug("Needy Knob Rules:\n" + this.NeedyKnobRuleSet.ToString());
			BombRules.logger.Debug("Button Rules:\n" + this.ButtonRuleSet.ToString());
			BombRules.logger.Debug("Wire Sequence Rules:\n" + this.WireSequenceRuleSet.ToString());
			BombRules.logger.Debug("Password Rules:\n" + this.PasswordRuleSet.ToString());
			BombRules.logger.Debug("Morse Code Rules:\n" + this.MorseCodeRuleSet.ToString());
			BombRules.logger.Debug("Venn Wire Rules:\n" + this.VennWireRuleSet.ToString());
			BombRules.logger.Debug("Maze Rules:\n" + this.MazeRuleSet.ToString());
			BombRules.logger.Debug("Simon Rules:\n" + this.SimonRuleSet.ToString());*/
		}

		//public ManualMetaData ManualMetaData;

		public WireRuleSet WireRuleSet;

		public WhosOnFirstRuleSet WhosOnFirstRuleSet;

		public KeypadRuleSet KeypadRuleSet;

		public MemoryRuleSet MemoryRuleSet;

		public NeedyKnobRuleSet NeedyKnobRuleSet;

		public ButtonRuleSet ButtonRuleSet;

		public WireSequenceRuleSet WireSequenceRuleSet;

		public PasswordRuleSet PasswordRuleSet;

		public MorseCodeRuleSet MorseCodeRuleSet;

		public VennWireRuleSet VennWireRuleSet;

		public MazeRuleSet MazeRuleSet;

		public SimonRuleSet SimonRuleSet;

		protected bool hasCachedStringValues;
	}
}
