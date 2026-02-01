using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{
	private const float MIN_DISTANCE_FROM_PLAYER = 10f;

	[SerializeField]
	private List<AiAgentType> _supportedAiAgentTypes = new List<AiAgentType>();

	public bool CanSpawnThisAgent(AiAgentType agentType)
	{
		return IsFarEnoughFromPlayer() && _supportedAiAgentTypes.Contains(agentType);
	}

	public void SpawnAiAgent(AiAgentBase aiAgentPrefab)
	{
		Instantiate(aiAgentPrefab, transform.position, transform.rotation);
	}

	private bool IsFarEnoughFromPlayer()
	{
		return Vector3.Distance(transform.position, PlayerSystemManager.Instance.PlayerController.transform.position) >= MIN_DISTANCE_FROM_PLAYER;
	}

	private void Awake()
	{
		AiAgentSystemManager.Instance.RegisterSpawner(this);
	}

	private void OnDestroy()
	{
		AiAgentSystemManager.Instance.UnregisterSpawner(this);
	}
}
