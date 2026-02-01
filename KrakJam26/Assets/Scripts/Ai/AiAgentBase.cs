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
		InitializeBlackboard();
		MaskSystemManager.Instance.OnMaskChangedEvent += OnMaskChanged;
		AiAgentSystemManager.Instance.RegisterAgent(this);
		_behaviorGraphAgent.enabled = true;
		_behaviorGraphAgent.Start();
	}

	private void InitializeBlackboard()
	{
		_behaviorGraphAgent.SetVariableValue("PlayerPawn", TargetPlayer.gameObject);
		OnMaskChanged(MaskSystemManager.Instance.CurrentMask);
	}

	private void OnMaskChanged(MaskType type)
	{
		switch (type)
		{
			case MaskType.Blind:
				ResetBlackboardVariables();
				_behaviorGraphAgent.SetVariableValue("IsPlayerBlind", true);
				break;
			case MaskType.Deaf:
				ResetBlackboardVariables();
				_behaviorGraphAgent.SetVariableValue("IsPlayerDeaf", true);
				break;
			case MaskType.Silent:
				ResetBlackboardVariables();
				_behaviorGraphAgent.SetVariableValue("IsPlayerSilent", true);
				break;
		}
	}
	
	private void ResetBlackboardVariables()
	{
		_behaviorGraphAgent.SetVariableValue("IsPlayerBlind", false);
		_behaviorGraphAgent.SetVariableValue("IsPlayerDeaf", false);
		_behaviorGraphAgent.SetVariableValue("IsPlayerSilent", false);
	}

	public void TriggerDeath()
	{
		_behaviorGraphAgent.End();
		AiAgentSystemManager.Instance.UnregisterAgent(this);
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		MaskSystemManager.Instance.OnMaskChangedEvent -= OnMaskChanged;
		AiAgentSystemManager.Instance.UnregisterAgent(this);
	}
}
