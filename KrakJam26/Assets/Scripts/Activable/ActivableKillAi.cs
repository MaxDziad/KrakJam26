using UnityEngine;

public class ActivableKillAi : ActivableBase
{
	[SerializeField]
	private AiAgentBase _aiAgent;

	public override void Activate()
	{
		if (_aiAgent != null)
		{
			_aiAgent.TriggerDeath();
		}
	}
}
