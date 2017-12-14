using System;
using System.Collections;
using System.Collections.Generic;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
	public class RuleUtil
	{
		public static string SubArgs(string input, Dictionary<string, object> args)
		{
			string text = input;
			int num = 0;
			List<object> list = new List<object>();
			foreach (KeyValuePair<string, object> keyValuePair in args)
			{
				text = text.Replace("{" + keyValuePair.Key + "}", keyValuePair.Value.ToString());
				text = text.Replace("{" + keyValuePair.Key, "{" + num.ToString());
				list.Add(keyValuePair.Value);
				num++;
			}
			text = string.Format(RuleUtil.pluralFormatProvider, text, list.ToArray());
			return text;
		}

		public static List<WireColor> GetListOfWireColors()
		{
			List<WireColor> list = new List<WireColor>();
			IEnumerator enumerator = Enum.GetValues(typeof(WireColor)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					list.Add((WireColor)obj);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return list;
		}

		protected static PluralFormatProvider pluralFormatProvider = new PluralFormatProvider();
	}
}
