using Unity.Behavior;
using UnityEngine;

public class AiAgentBase : MonoBehaviour
{
	[SerializeField]
	private AiAgentType _agentType;

	private BehaviorGraphAgent _behaviorGraphAgent;

	public PlayerController TargetPlayer { get; private set; }
	public AiAgentType AgentType => AgentType;

	private void Awake()
	{
		_behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
	}

	private void Start()
	{
		TargetPlayer = PlayerSystemManager.Instance.PlayerController;
		AiAgentSystemManager.Instance.RegisterAgent(this);
		_behaviorGraphAgent.SetVariableValue("PlayerPawn", TargetPlayer.gameObject);
		_behaviorGraphAgent.Start();
	}

	public void TriggerDeath()
	{
		_behaviorGraphAgent.End();
		AiAgentSystemManager.Instance.UnregisterAgent(this);
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		AiAgentSystemManager.Instance.UnregisterAgent(this);
	}
}
