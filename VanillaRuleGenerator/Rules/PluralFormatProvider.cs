using System;

namespace VanillaRuleGenerator.Rules
{
	public class PluralFormatProvider : IFormatProvider, ICustomFormatter
	{
		public object GetFormat(Type formatType)
		{
			return this;
		}

		public string Format(string formatString, object arg, IFormatProvider formatProvider)
		{
			string[] array = formatString.Split(new char[]
			{
				';'
			});
			if (array.Length > 1)
			{
				int num = (int)arg;
				int num2 = (num != 1) ? 1 : 0;
				return array[num2];
			}
			return arg.ToString();
		}
	}
}
