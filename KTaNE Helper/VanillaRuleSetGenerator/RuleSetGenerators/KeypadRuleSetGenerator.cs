using System;
using System.Collections.Generic;

namespace Assets.Scripts.Rules
{
	public class KeypadRuleSetGenerator : AbstractRuleSetGenerator
	{
		protected override AbstractRuleSet CreateRules(bool useDefault)
		{
			KeypadRuleSet keypadRuleSet = new KeypadRuleSet();
			this.PopulatePLists(keypadRuleSet.PrecedenceLists);
			this.PopulateFileNames(keypadRuleSet.PrecedenceLists, keypadRuleSet.FileNames);
			return keypadRuleSet;
		}

		private void PopulateFileNames(List<List<string>> pLists, List<List<string>> fileNames)
		{
			foreach (List<string> list in pLists)
			{
				List<string> list2 = new List<string>();
				fileNames.Add(list2);
				foreach (string symbol in list)
				{
					list2.Add(SymbolPool.GetFileName(symbol));
				}
			}
		}

		private void PopulatePLists(List<List<string>> pLists)
		{
			List<string> list = new List<string>(SymbolPool.Symbols);
			List<string> list2 = new List<string>();
			for (int i = 0; i < 6; i++)
			{
				List<string> list3 = new List<string>();
				pLists.Add(list3);
				int j = 0;
				if (i > 0)
				{
					while (j < 3)
					{
						if (list2.Count == 0)
						{
							break;
						}
						int index = this.rand.Next(list2.Count);
						list3.Add(list2[index]);
						list2.RemoveAt(index);
						j++;
					}
				}
				while (j < 7)
				{
					if (list.Count > 0)
					{
						int index2 = this.rand.Next(list.Count);
						list3.Add(list[index2]);
						list2.Add(list[index2]);
						list.RemoveAt(index2);
					}
					else
					{
						//KeypadRuleSetGenerator.Logger.Info("Insufficient symbols for keypad rule generation");
					}
					j++;
				}
				this.RandomizePList(list3);
			}
		}

		private void RandomizePList(List<string> pList)
		{
			for (int i = 0; i < pList.Count; i++)
			{
				string value = pList[i];
				int index = this.rand.Next(pList.Count);
				pList[i] = pList[index];
				pList[index] = value;
			}
		}

		private const int MAX_LISTS = 6;

		private const int MAX_SYMBOLS = 7;

		private const int OVERLAP = 3;
	}
}
