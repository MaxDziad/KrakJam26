using System.Collections.Generic;
using UnityEngine;

public class AiAgentSystemManager : MonoBehaviour
{
	public static AiAgentSystemManager Instance { get; private set; }

	private Dictionary<AiAgentType, List<AiAgentBase>> _registeredAgents =
						new Dictionary<AiAgentType, List<AiAgentBase>>();

	public void RegisterAgent(AiAgentBase agent)
	{
		if (_registeredAgents.TryGetValue(agent.AgentType, out var agentsList))
		{
			agentsList.Add(agent);
		}
	}

	public void UnregisterAgent(AiAgentBase agent)
	{
		if (_registeredAgents.TryGetValue(agent.AgentType, out var agentsList))
		{
			agentsList.Remove(agent);
		}
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		InitializeDictionary();
	}

	private void InitializeDictionary()
	{
		_registeredAgents.Add(AiAgentType.InvisibleGuy, new List<AiAgentBase>());
		_registeredAgents.Add(AiAgentType.Ghost, new List<AiAgentBase>());
		_registeredAgents.Add(AiAgentType.ShyGuy, new List<AiAgentBase>());
	}
}
