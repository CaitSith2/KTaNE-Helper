using System;
using VanillaRuleModifierAssembly.RuleSetGenerators;

namespace Assets.Scripts.Rules
{
	public class RuleManager
	{
		protected RuleManager()
		{
			this.wireRuleSetGenerator = new WireRuleGenerator();
			this.whosOnFirstRuleSetGenerator = new WhosOnFirstRuleSetGenerator();
			/*this.memoryRuleSetGenerator = new MemoryRuleSetGenerator();
			this.keypadRuleSetGenerator = new KeypadRuleSetGenerator();
			this.needyKnobRuleSetGenerator = new NeedyKnobRuleSetGenerator();
			this.buttonRuleSetGenerator = new ButtonRuleSetGenerator();
			this.wireSequenceRuleSetGenerator = new WireSequenceRuleSetGenerator();
			this.passwordRuleSetGenerator = new PasswordRuleSetGenerator();
			this.morseCodeRuleSetGenerater = new MorseCodeRuleSetGenerator();
			this.vennWireRuleSetGenerator = new VennWireRuleSetGenerator();
			this.rhythmHeavenRuleSetGenerator = new RhythmHeavenRuleSetGenerator();
			this.mazeRuleSetGenerator = new MazeRuleSetGenerator();
			this.simonRuleSetGenerator = new SimonRuleSetGenerator();*/
		}

		public static RuleManager Instance
		{
			get
			{
				if (RuleManager.instance == null)
				{
					RuleManager.instance = new RuleManager();
				}
				return RuleManager.instance;
			}
		}

	    private BombRules _currentRules;
	    public BombRules CurrentRules
	    {
	        get
	        {
	            if (_currentRules == null)
	            {
	                Initialize(1);
	            }
	            return _currentRules;
	        } 
	    }

	    public WireRuleSet WireRuleSet
        {
            get
            {
                return this.CurrentRules.WireRuleSet;
            }
        }

		public WhosOnFirstRuleSet WhosOnFirstRuleSet => this.CurrentRules.WhosOnFirstRuleSet;

	    /*public KeypadRuleSet KeypadRuleSet
		{
			get
			{
				return this.CurrentRules.KeypadRuleSet;
			}
		}

		public MemoryRuleSet MemoryRuleSet
		{
			get
			{
				return this.CurrentRules.MemoryRuleSet;
			}
		}

		public NeedyKnobRuleSet NeedyKnobRuleSet
		{
			get
			{
				return this.CurrentRules.NeedyKnobRuleSet;
			}
		}

		public ButtonRuleSet ButtonRuleSet
		{
			get
			{
				return this.CurrentRules.ButtonRuleSet;
			}
		}

		public WireSequenceRuleSet WireSequenceRuleSet
		{
			get
			{
				return this.CurrentRules.WireSequenceRuleSet;
			}
		}

		public PasswordRuleSet PasswordRuleSet
		{
			get
			{
				return this.CurrentRules.PasswordRuleSet;
			}
		}

		public MorseCodeRuleSet MorseCodeRuleSet
		{
			get
			{
				return this.CurrentRules.MorseCodeRuleSet;
			}
		}

		public VennWireRuleSet VennWireRuleSet
		{
			get
			{
				return this.CurrentRules.VennWireRuleSet;
			}
		}

		public RhythmHeavenRuleSet RhythmHeavenRuleSet
		{
			get
			{
				return this.CurrentRules.RhythmHeavenRuleSet;
			}
		}

		public MazeRuleSet MazeRuleSet
		{
			get
			{
				return this.CurrentRules.MazeRuleSet;
			}
		}

		public SimonRuleSet SimonRuleSet
		{
			get
			{
				return this.CurrentRules.SimonRuleSet;
			}
		}*/

		public int Seed { get; protected set; }

		public void Initialize(int seed)
		{
			if (seed == this.Seed)
			{
				return;
			}
			this.Seed = seed;
		    if (seed == int.MinValue)
		        seed = 0;
			this._currentRules = this.GenerateBombRules(seed);
		}

		public BombRules GenerateBombRules(int seed)
		{
			BombRules bombRules = new BombRules();
			/*bombRules.ManualMetaData = new ManualMetaData
			{
				BombClassification = ManualMetaData.BOMB_CLASSIFICATION,
				LockCode = ManualMetaData.LOCK_CODE
			};*/
			bombRules.WireRuleSet = (WireRuleSet)this.wireRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.WhosOnFirstRuleSet = (WhosOnFirstRuleSet)this.whosOnFirstRuleSetGenerator.GenerateRuleSet(seed);
			/*bombRules.MemoryRuleSet = (MemoryRuleSet)this.memoryRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.KeypadRuleSet = (KeypadRuleSet)this.keypadRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.NeedyKnobRuleSet = (NeedyKnobRuleSet)this.needyKnobRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.ButtonRuleSet = (ButtonRuleSet)this.buttonRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.WireSequenceRuleSet = (WireSequenceRuleSet)this.wireSequenceRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.PasswordRuleSet = (PasswordRuleSet)this.passwordRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.MorseCodeRuleSet = (MorseCodeRuleSet)this.morseCodeRuleSetGenerater.GenerateRuleSet(seed);
			bombRules.VennWireRuleSet = (VennWireRuleSet)this.vennWireRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.RhythmHeavenRuleSet = (RhythmHeavenRuleSet)this.rhythmHeavenRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.MazeRuleSet = (MazeRuleSet)this.mazeRuleSetGenerator.GenerateRuleSet(seed);
			bombRules.SimonRuleSet = (SimonRuleSet)this.simonRuleSetGenerator.GenerateRuleSet(seed);*/
			bombRules.CacheStringValues();
			bombRules.PrintRules();
			return bombRules;
		}

	    public static bool SeedIsVanilla => (Instance.Seed == 1 || Instance.Seed == 2 || Instance.Seed < -2);
	    public static bool SeedIsModded => !SeedIsVanilla;

	    protected static RuleManager instance;

		public static readonly int DEFAULT_SEED = 1;

		protected Random rand;

		protected WireRuleGenerator wireRuleSetGenerator;

		protected WhosOnFirstRuleSetGenerator whosOnFirstRuleSetGenerator;

		/*protected MemoryRuleSetGenerator memoryRuleSetGenerator;

		protected KeypadRuleSetGenerator keypadRuleSetGenerator;

		protected NeedyKnobRuleSetGenerator needyKnobRuleSetGenerator;

		protected ButtonRuleSetGenerator buttonRuleSetGenerator;

		protected WireSequenceRuleSetGenerator wireSequenceRuleSetGenerator;

		protected PasswordRuleSetGenerator passwordRuleSetGenerator;

		protected MorseCodeRuleSetGenerator morseCodeRuleSetGenerater;

		protected VennWireRuleSetGenerator vennWireRuleSetGenerator;

		protected RhythmHeavenRuleSetGenerator rhythmHeavenRuleSetGenerator;

		protected MazeRuleSetGenerator mazeRuleSetGenerator;

		protected SimonRuleSetGenerator simonRuleSetGenerator;*/

		protected bool initialized;
	}
}
