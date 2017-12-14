using System.Collections.Generic;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
	public class VennWireRuleSet : AbstractRuleSet
	{
		public VennWireRuleSet(Dictionary<VennWireState, CutInstruction> ruleDict)
		{
			this.RuleDict = ruleDict;
			this.batteryQueryArgs = new Dictionary<string, object>();
			this.batteryQueryArgs.Add("batteryCount", 1);
			this.batteryQuery = new Query
			{
				Property = QueryableProperty.MoreThanXBatteries,
				Args = this.batteryQueryArgs
			};
		}

		public Dictionary<VennWireState, CutInstruction> RuleDict { get; protected set; }

		public List<VennWireState> GetStatesThatRequiresCutting()
		{
			List<VennWireState> list = new List<VennWireState>();
			foreach (KeyValuePair<VennWireState, CutInstruction> keyValuePair in this.RuleDict)
			{
				if (keyValuePair.Value == CutInstruction.Cut)
				{
					list.Add(keyValuePair.Key);
				}
			}
			return list;
		}

		public bool ShouldWireBeSnipped(VennWireComponent component, int indexOfCutWire, bool log)
		{
			VennSnippableWire wire = null;
			foreach (VennSnippableWire vennSnippableWire in component.ActiveWires)
			{
				if (indexOfCutWire == vennSnippableWire.WireIndex)
				{
					wire = vennSnippableWire;
					break;
				}
			}
			CutInstruction cutInstructionForWire = this.GetCutInstructionForWire(component, wire, log);
			return this.ShouldCut(component, cutInstructionForWire);
		}

		protected CutInstruction GetCutInstructionForWire(VennWireComponent component, VennSnippableWire wire, bool log)
		{
			bool flag = (wire.Color & VennWireColor.Red) == VennWireColor.Red;
			bool flag2 = (wire.Color & VennWireColor.Blue) == VennWireColor.Blue;
			bool hasSymbol = wire.HasSymbol;
			bool isLEDOn = wire.IsLEDOn;
			return this.RuleDict[new VennWireState(flag, flag2, hasSymbol, isLEDOn)];
		}

		protected bool ShouldCut(VennWireComponent component, CutInstruction instruction)
		{
			switch (instruction)
			{
			case CutInstruction.Cut:
				return true;
			case CutInstruction.DoNotCut:
				return false;
			case CutInstruction.CutIfSerialEven:
			    return SerialNumber.SerialNumberLastDigitEven();
			case CutInstruction.CutIfParallelPortPresent:
			    return PortPlate.IsPortPresent(PortTypes.Parallel);
			case CutInstruction.CutIfTwoOrMoreBatteriesPresent:
			    return Batteries.TotalBatteries >= 2;
			default:
				return false;
			}
		}

		public override string ToString()
		{
			string text = string.Empty;
			foreach (KeyValuePair<VennWireState, CutInstruction> keyValuePair in this.RuleDict)
			{
				text += string.Format("[{0}]: {1}\n", keyValuePair.Key, keyValuePair.Value.ToString());
			}
			return text;
		}

		protected Query batteryQuery;

		protected Dictionary<string, object> batteryQueryArgs;
	}
}
