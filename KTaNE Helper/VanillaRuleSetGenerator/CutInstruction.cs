using System;

namespace Assets.Scripts.Components.VennWire
{
	public enum CutInstruction
	{
		Cut,
		DoNotCut,
		CutIfSerialEven,
		CutIfParallelPortPresent,
		CutIfTwoOrMoreBatteriesPresent
	}
}
