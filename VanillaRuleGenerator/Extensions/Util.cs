using System;
using System.Collections;
using System.Text;

namespace VanillaRuleGenerator.Extensions
{
	public class Util
	{
		public static string Ordinal(int number)
		{
			string arg = string.Empty;
			int num = number % 10;
			int num2 = (int)Math.Floor(number / 10m) % 10;
			if (num2 == 1)
			{
				arg = "th";
			}
			else
			{
				switch (num)
				{
				case 1:
					arg = "st";
					break;
				case 2:
					arg = "nd";
					break;
				case 3:
					arg = "rd";
					break;
				default:
					arg = "th";
					break;
				}
			}
			return string.Format("{0}{1}", number, arg);
		}

		public static string OrdinalWord(int number)
		{
			switch (number)
			{
			case 1:
				return "First";
			case 2:
				return "Second";
			case 3:
				return "Third";
			case 4:
				return "Fourth";
			case 5:
				return "Fifth";
			case 6:
				return "Sixth";
			case 7:
				return "Seventh";
			case 8:
				return "Eighth";
			case 9:
				return "Ninth";
			default:
				return string.Empty;
			}
		}

		public static IEnumerator PerformDelegateAfterNextUpdate(Action onComplete)
		{
			yield return null;
			onComplete();
			yield break;
		}

		public static string WrapText(string text, int maxChars)
		{
			string[] array = text.Split(new char[]
			{
				' '
			});
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string text2 in array)
			{
				num += text2.Length;
				if (num > maxChars)
				{
					stringBuilder.Append("\n");
					num = text2.Length;
				}
				stringBuilder.Append(text2);
				stringBuilder.Append(" ");
				num++;
			}
			return stringBuilder.ToString();
		}

		public static bool IsFlagSet<T>(T flags, T flag) where T : struct
		{
			int num = (int)((object)flags);
			int num2 = (int)((object)flag);
			return (num & num2) != 0;
		}

		public static void SetFlag<T>(ref T flags, T flag) where T : struct
		{
			int num = (int)((object)flags);
			int num2 = (int)((object)flag);
			flags = (T)((object)(num | num2));
		}

		public static void UnsetFlag<T>(ref T flags, T flag) where T : struct
		{
			int num = (int)((object)flags);
			int num2 = (int)((object)flag);
			flags = (T)((object)(num & ~num2));
		}

		public static void ToggleFlag<T>(ref T flags, T flag) where T : struct
		{
			if (Util.IsFlagSet<T>(flags, flag))
			{
				Util.UnsetFlag<T>(ref flags, flag);
			}
			else
			{
				Util.SetFlag<T>(ref flags, flag);
			}
		}

		public static string GetResultsFormattedTimeRemaining(TimeSpan span)
		{
			if (span.CompareTo(TimeSpan.Zero) < 0)
			{
				span = TimeSpan.Zero;
			}
			if (span.TotalMinutes >= 1.0)
			{
				return string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
			}
			int num = span.Milliseconds;
			num /= 10;
			return string.Format("{0:0}.{1:00}s", span.Seconds, num);
		}

		public static string FormatFileSize(ulong fileSizeInBytes)
		{
			string[] array = new string[]
			{
				"B",
				"KB",
				"MB",
				"GB",
				"TB",
				"PB",
				"EB"
			};
			if (fileSizeInBytes == 0UL)
			{
				return "0" + array[0];
			}
			int num = Convert.ToInt32(Math.Floor(Math.Log(fileSizeInBytes, 1024.0)));
			return Math.Round(fileSizeInBytes / Math.Pow(1024.0, (double)num), 1).ToString() + array[num];
		}

		public static bool SHOW_DEBUG_MISSIONS;
	}
}
