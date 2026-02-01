using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldAiSpawnConfig", menuName = "ScriptableObjects/WorldAiSpawnConfig", order = 1)]
public class WorldAiSpawnConfig : ScriptableObject
{
	[Serializable]
	private struct AiSpawnInfo
	{
		[SerializeField]
		public AiAgentType AiType;

		[SerializeField]
		public float SpawnProbability;

		[SerializeField]
		public int MaxSpawnedAtATime;
	}

	[SerializeField]
	[Header("Sum of probability should be 100!")]
	private List<AiSpawnInfo> _spawnableAiTypes;

	[SerializeField]
	private float _cooldownBetweenSpawns = 5f;

	[SerializeField]
	[Range(1, 10)]
	private int _numberOfAgentSpawnedPerCycle = 1;

	public float CooldownBetweenSpawns => _cooldownBetweenSpawns;

	public List<AiAgentType> GetAisToSpawn(Dictionary<AiAgentType, int> currentCounts)
	{
		List<AiAgentType> aisToSpawn = new List<AiAgentType>();

		for (int i = 0; i < _numberOfAgentSpawnedPerCycle; i++)
		{
			float totalProbability = UnityEngine.Random.Range(0f, 100f);
			float cumulativeProbability = 0f;

			foreach (var aiInfo in _spawnableAiTypes)
			{
				cumulativeProbability += aiInfo.SpawnProbability;

				if (currentCounts[aiInfo.AiType] >= aiInfo.MaxSpawnedAtATime)
				{
					continue;
				}

				if (totalProbability <= cumulativeProbability)
				{
					aisToSpawn.Add(aiInfo.AiType);
					break;
				}
			}
		}

		return aisToSpawn;
	}
}
