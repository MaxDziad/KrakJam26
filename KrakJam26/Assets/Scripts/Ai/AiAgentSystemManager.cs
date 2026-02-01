using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiAgentSystemManager : MonoBehaviour
{
	public static AiAgentSystemManager Instance { get; private set; }

	[SerializeField]
	private AiAgentPrefabs _aiAgentPrefabs;

	private Dictionary<AiAgentType, HashSet<AiAgentBase>> _registeredAgents =
						new Dictionary<AiAgentType, HashSet<AiAgentBase>>();

	private List<AiSpawner> _registeredSpawners = new List<AiSpawner>();
	private WorldAiSpawnConfig _currentWorldAiSpawnConfig;
	private Coroutine _spawnRoutine;

	public void SetCurrentWorldAiSpawnConfig(WorldAiSpawnConfig config)
	{
		_currentWorldAiSpawnConfig = config;
	}

	public void RegisterAgent(AiAgentBase agent)
	{
		if (_registeredAgents.TryGetValue(agent.AgentType, out var agentsSet))
		{
			agentsSet.Add(agent);
		}
	}

	public void UnregisterAgent(AiAgentBase agent)
	{
		if (_registeredAgents.TryGetValue(agent.AgentType, out var agentsSet))
		{
			agentsSet.Remove(agent);
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
		_registeredAgents.Add(AiAgentType.InvisibleGuy, new HashSet<AiAgentBase>());
		_registeredAgents.Add(AiAgentType.Ghost, new HashSet<AiAgentBase>());
		_registeredAgents.Add(AiAgentType.ShyGuy, new HashSet<AiAgentBase>());
	}

	public void RegisterSpawner(AiSpawner aiSpawner)
	{
		if (aiSpawner != null)
		{
			_registeredSpawners.Add(aiSpawner);
		}
	}

	public void UnregisterSpawner(AiSpawner aiSpawner)
	{
		_registeredSpawners.Remove(aiSpawner);
	}

	public void StartSpawningAis()
	{
		if (_spawnRoutine == null)
		{
			_spawnRoutine = StartCoroutine(SpawnLoop());
		}
	}

	public void StopSpawningAis()
	{
		if (_spawnRoutine != null)
		{
			StopCoroutine(_spawnRoutine);
			_spawnRoutine = null;
		}
	}

	private IEnumerator SpawnLoop()
	{
		while (true)
		{
			if (_currentWorldAiSpawnConfig != null && _registeredSpawners.Count > 0)
			{
				Dictionary<AiAgentType, int> currentCounts = _registeredAgents.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value.Count);
				List<AiAgentType> typesToSpawn = _currentWorldAiSpawnConfig.GetAisToSpawn(currentCounts);

				foreach (var type in typesToSpawn)
				{
					SpawnAgent(type);
				}
			}

			yield return new WaitForSeconds(_currentWorldAiSpawnConfig?.CooldownBetweenSpawns ?? 5f);
		}
	}

	private void SpawnAgent(AiAgentType type)
	{
		var randomSpawner = _registeredSpawners[Random.Range(0, _registeredSpawners.Count)];
		AiAgentBase prefab = _aiAgentPrefabs.GetAiAgentPrefab(type);

		if (prefab != null)
		{
			randomSpawner.SpawnAiAgent(prefab);
		}
	}
}
