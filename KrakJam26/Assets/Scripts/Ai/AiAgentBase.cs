using Unity.Behavior;
using UnityEngine;

public class AiAgentBase : MonoBehaviour
{
	[SerializeField]
	private AiAgentType AgentType;

	private BehaviorGraphAgent _behaviorGraphAgent;

	public PlayerController TargetPlayer { get; private set; }

	private void Awake()
	{
		_behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
	}

	private void Start()
	{
		TargetPlayer = PlayerSystemManager.Instance.PlayerController;
		_behaviorGraphAgent.SetVariableValue("PlayerPawn", TargetPlayer.gameObject);
		_behaviorGraphAgent.Start();
	}

	public void TriggerDeath()
	{
		_behaviorGraphAgent.End();
		Destroy(gameObject);
	}
}
