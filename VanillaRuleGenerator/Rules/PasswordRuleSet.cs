using System.Collections.Generic;

namespace VanillaRuleGenerator.Rules
{
	public class PasswordRuleSet : AbstractRuleSet
	{
		public PasswordRuleSet(List<string> possibleWords)
		{
			this.possibilities = new List<string>(possibleWords);
		}





		private List<string> GetMatches(List<List<char>> charValues)
		{
			List<string> list = new List<string>();
			foreach (string text in this.possibilities)
			{
				char[] array = text.ToCharArray();
				bool flag = true;
				for (int i = 0; i < charValues.Count; i++)
				{
					bool flag2 = false;
					for (int j = 0; j < charValues[i].Count; j++)
					{
						if (charValues[i][j] == array[i] || charValues[i].Count == 1)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(text);
				}
			}
			return list;
		}


		public override string ToString()
		{
			return "Possibilities: " + string.Join(", ", this.possibilities.ToArray());
		}

		private List<char> alphabet = new List<char>
		{
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z'
		};

		private const int NUM_CHARACTERS = 5;

		public int CharactersPerPosition = 5;

		public List<string> possibilities;
	}
}
