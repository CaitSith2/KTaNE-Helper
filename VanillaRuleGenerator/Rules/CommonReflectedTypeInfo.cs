using VanillaRuleGenerator.Extensions;

namespace VanillaRuleGenerator.Rules
{
    public class CommonReflectedTypeInfo
    {
        public static bool IsVanillaSeed => RuleManager.SeedIsVanilla;
        public static bool IsModdedSeed => RuleManager.SeedIsModded;

        public static void DebugLog(string message, params object[] args)
        {
            Debug.LogFormat("[KTaNE Helper] {0}", new object[] { string.Format(message, args) });
        }
    }
}