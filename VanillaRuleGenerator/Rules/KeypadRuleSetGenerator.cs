using System.Collections.Generic;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace VanillaRuleGenerator.Rules
{
	public class KeypadRuleSetGenerator : AbstractRuleSetGenerator
	{
		protected override AbstractRuleSet CreateRules(bool useDefault)
		{
			KeypadRuleSet keypadRuleSet = new KeypadRuleSet();
			PopulatePLists(keypadRuleSet.PrecedenceLists);
			PopulateFileNames(keypadRuleSet.PrecedenceLists, keypadRuleSet.FileNames);
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
			List<string> unusedSymbols = new List<string>(SymbolPool.Symbols);
			List<string> unusedOverlappingSymbols = new List<string>();
			for (int i = 0; i < MAX_LISTS; i++)
			{
				List<string> pList = new List<string>();
				pLists.Add(pList);
				int j = 0;
				if (i > 0)
				{
					while (j < OVERLAP)
					{
						if (unusedOverlappingSymbols.Count == 0)
						{
							break;
						}
						int index = rand.Next(unusedOverlappingSymbols.Count);
						pList.Add(unusedOverlappingSymbols[index]);
						unusedOverlappingSymbols.RemoveAt(index);
						j++;
					}
				}
				while (j < MAX_SYMBOLS)
				{
					if (unusedSymbols.Count > 0)
					{
						int index2 = rand.Next(unusedSymbols.Count);
						pList.Add(unusedSymbols[index2]);
						unusedOverlappingSymbols.Add(unusedSymbols[index2]);
						unusedSymbols.RemoveAt(index2);
					}
					else
					{
						//KeypadRuleSetGenerator.Logger.Info("Insufficient symbols for keypad rule generation");
					}
					j++;
				}
				RandomizePList(pList);
			}
		}

		private void RandomizePList(List<string> pList)
		{
			for (int i = 0; i < pList.Count; i++)
			{
				string value = pList[i];
				int index = rand.Next(pList.Count);
				pList[i] = pList[index];
				pList[index] = value;
			}
		}

		private const int MAX_LISTS = 6;

		private const int MAX_SYMBOLS = 7;

		private const int OVERLAP = 3;
	}
}
