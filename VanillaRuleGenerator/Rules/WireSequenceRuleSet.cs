using System.Collections.Generic;
using System.Text;
using VanillaRuleGenerator.Extensions;
using VanillaRuleGenerator.Rules.BombGame;

namespace VanillaRuleGenerator.Rules
{
	public class WireSequenceRuleSet : AbstractRuleSet
	{
		public WireSequenceRuleSet(IList<int>[] redWiresToSnip, IList<int>[] blueWiresToSnip, IList<int>[] blackWiresToSnip)
		{
			this.redWiresToSnip = redWiresToSnip;
			this.blueWiresToSnip = blueWiresToSnip;
			this.blackWiresToSnip = blackWiresToSnip;
		}

		public bool ShouldBeSnipped(WireColor color, int number, int to)
		{
			IList<int>[] array;
			switch (color)
			{
			case WireColor.black:
				array = this.blackWiresToSnip;
				goto IL_3B;
			case WireColor.blue:
				array = this.blueWiresToSnip;
				goto IL_3B;
			}
			array = this.redWiresToSnip;
			IL_3B:
			bool result = false;
			if (number < array.Length && array[number].Contains(to))
			{
				result = true;
			}
			return result;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < WireSequenceRuleSetGenerator.NUM_COLOURS; i++)
			{
				WireColor color = (WireColor)i;
				stringBuilder.AppendFormat("{0} Wires: ", color.ToString().ToUpperInvariant());
				for (int j = 0; j < WireSequenceRuleSetGenerator.NumWiresPerColour; j++)
				{
					stringBuilder.AppendFormat("{0}: [", Util.OrdinalWord(j + 1));
					List<char> list = new List<char>(3);
					for (int k = 0; k < WireSequenceRuleSetGenerator.NUM_PER_PAGE; k++)
					{
						if (this.ShouldBeSnipped(color, j, k))
						{
							switch (k)
							{
							case 0:
								list.Add('A');
								goto IL_A9;
							case 1:
								list.Add('B');
								goto IL_A9;
							}
							list.Add('C');
						}
						IL_A9:;
					}
					for (int l = 0; l < list.Count; l++)
					{
						stringBuilder.Append(list[l]);
						if (l == list.Count - 2)
						{
							stringBuilder.Append(" or ");
						}
						else if (l < list.Count - 2 && list.Count > 2)
						{
							stringBuilder.Append(", ");
						}
					}
					stringBuilder.Append("], ");
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		public IList<int>[] redWiresToSnip;

		public IList<int>[] blueWiresToSnip;

		public IList<int>[] blackWiresToSnip;
	}
}
