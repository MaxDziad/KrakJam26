using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class AiAgentBase : MonoBehaviour
{
	[SerializeField]
	private AiAgentType _agentType;

	private BehaviorGraphAgent _behaviorGraphAgent;
	private NavMeshAgent _navmeshAgent;

	public PlayerController TargetPlayer { get; private set; }
	public AiAgentType AgentType => _agentType;
	public NavMeshAgent NavmeshAgent => _navmeshAgent ??= GetComponent<NavMeshAgent>();

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
