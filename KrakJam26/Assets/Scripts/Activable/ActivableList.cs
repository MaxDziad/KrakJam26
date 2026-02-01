using System.Collections.Generic;
using UnityEngine;

public class ActivableList : ActivableBase
{
	[SerializeField]
	private List<ActivableBase> _activables;
	public override void Activate()
	{
		foreach (var activable in _activables)
		{
			activable.Activate();
		}
	}

	public override void Deactivate()
	{
		foreach (var activable in _activables)
		{
			activable.Deactivate();
		}
	}
}
