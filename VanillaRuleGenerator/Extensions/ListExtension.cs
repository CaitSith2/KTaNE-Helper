using System.Collections.Generic;
using VanillaRuleGenerator.Helpers;

namespace VanillaRuleGenerator.Extensions
{
    public static partial class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list, MonoRandom random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}