using System;
using VanillaRuleGenerator.Rules;

namespace VanillaRuleGenerator
{
	public class RuleManager
	{
		protected RuleManager()
		{
			WireRuleSetGenerator = new WireRuleGenerator();
			WhosOnFirstRuleSetGenerator = new WhosOnFirstRuleSetGenerator();
			MemoryRuleSetGenerator = new MemoryRuleSetGenerator();
			KeypadRuleSetGenerator = new KeypadRuleSetGenerator();
			NeedyKnobRuleSetGenerator = new NeedyKnobRuleSetGenerator();
			ButtonRuleSetGenerator = new ButtonRuleGenerator();
			WireSequenceRuleSetGenerator = new WireSequenceRuleSetGenerator();
			PasswordRuleSetGenerator = new PasswordRuleGenerator();
			MorseCodeRuleSetGenerater = new MorseCodeRuleGenerator();
			VennWireRuleSetGenerator = new VennWireGenerator();
			MazeRuleSetGenerator = new MazeRuleSetGenerator();
			SimonRuleSetGenerator = new SimonRuleGenerator();
		}

	    public static RuleManager Instance
	    {
	        get
	        {
	            if (_instance != null) return _instance;
	            _instance = new RuleManager();
	            _instance.Initialize(1);
	            return _instance;
	        }
	    }

	    public BombRules CurrentRules { get; protected set; }

	    public WireRuleSet WireRuleSet => CurrentRules.WireRuleSet;
	    public WhosOnFirstRuleSet WhosOnFirstRuleSet => CurrentRules.WhosOnFirstRuleSet;
	    public KeypadRuleSet KeypadRuleSet => CurrentRules.KeypadRuleSet;
	    public MemoryRuleSet MemoryRuleSet => CurrentRules.MemoryRuleSet;
	    public NeedyKnobRuleSet NeedyKnobRuleSet => CurrentRules.NeedyKnobRuleSet;
	    public ButtonRuleSet ButtonRuleSet => CurrentRules.ButtonRuleSet;
	    public WireSequenceRuleSet WireSequenceRuleSet => CurrentRules.WireSequenceRuleSet;
	    public PasswordRuleSet PasswordRuleSet => CurrentRules.PasswordRuleSet;
	    public MorseCodeRuleSet MorseCodeRuleSet => CurrentRules.MorseCodeRuleSet;
	    public VennWireRuleSet VennWireRuleSet => CurrentRules.VennWireRuleSet;
	    public MazeRuleSet MazeRuleSet => CurrentRules.MazeRuleSet;
	    public SimonRuleSet SimonRuleSet => CurrentRules.SimonRuleSet;

	    public int Seed { get; protected set; }

		public void Initialize(int seed)
		{
			if (seed == Seed || (seed == int.MinValue && Seed == 0))
			{
				return;
			}
			Seed = seed;
		    if (seed == int.MinValue)
		        seed = 0;
			CurrentRules = GenerateBombRules(seed);
		}

		public BombRules GenerateBombRules(int seed)
		{
		    BombRules bombRules = new BombRules
		    {
		        WireRuleSet = (WireRuleSet) WireRuleSetGenerator.CreateWireRules(seed),
		        WhosOnFirstRuleSet = (WhosOnFirstRuleSet) WhosOnFirstRuleSetGenerator.GenerateRuleSet(seed),
		        MemoryRuleSet = (MemoryRuleSet) MemoryRuleSetGenerator.GenerateRuleSet(seed),
		        KeypadRuleSet = (KeypadRuleSet) KeypadRuleSetGenerator.GenerateRuleSet(seed),
		        NeedyKnobRuleSet = (NeedyKnobRuleSet) NeedyKnobRuleSetGenerator.GenerateRuleSet(seed),
		        ButtonRuleSet = (ButtonRuleSet) ButtonRuleSetGenerator.GenerateButtonRules(seed),
		        WireSequenceRuleSet = (WireSequenceRuleSet) WireSequenceRuleSetGenerator.GenerateRuleSet(seed),
		        PasswordRuleSet = (PasswordRuleSet) PasswordRuleSetGenerator.GeneratePasswordRules(seed),
		        MorseCodeRuleSet = (MorseCodeRuleSet) MorseCodeRuleSetGenerater.GenerateRuleSet(seed),
		        VennWireRuleSet = (VennWireRuleSet) VennWireRuleSetGenerator.GenerateVennWireRules(seed),
		        MazeRuleSet = (MazeRuleSet) MazeRuleSetGenerator.GenerateRuleSet(seed),
		        SimonRuleSet = (SimonRuleSet) SimonRuleSetGenerator.GenerateSimonRuleSet(seed)
		    };
		    bombRules.CacheStringValues();
			bombRules.PrintRules();
			return bombRules;
		}

	    public static bool SeedIsVanilla => (Instance.Seed == 1 || Instance.Seed == 2 || Instance.Seed < -2);
	    public static bool SeedIsModded => !SeedIsVanilla;

	    private static RuleManager _instance;

		public static readonly int DefaultSeed = 1;

		protected Random Rand;

		protected WireRuleGenerator WireRuleSetGenerator;
		protected WhosOnFirstRuleSetGenerator WhosOnFirstRuleSetGenerator;
		protected MemoryRuleSetGenerator MemoryRuleSetGenerator;
		protected KeypadRuleSetGenerator KeypadRuleSetGenerator;
		protected NeedyKnobRuleSetGenerator NeedyKnobRuleSetGenerator;
		protected ButtonRuleGenerator ButtonRuleSetGenerator;
		protected WireSequenceRuleSetGenerator WireSequenceRuleSetGenerator;
		protected PasswordRuleGenerator PasswordRuleSetGenerator;
		protected MorseCodeRuleGenerator MorseCodeRuleSetGenerater;
		protected VennWireGenerator VennWireRuleSetGenerator;
		protected MazeRuleSetGenerator MazeRuleSetGenerator;
		protected SimonRuleGenerator SimonRuleSetGenerator;

		protected bool Initialized;
	}
}
