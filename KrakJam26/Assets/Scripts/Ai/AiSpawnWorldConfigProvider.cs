using UnityEngine;

public class AiSpawnWorldConfigProvider : MonoBehaviour
{
	[SerializeField]
	private WorldAiSpawnConfig _worldAiSpawnConfig;

	private void Awake()
	{
		AiAgentSystemManager.Instance.SetCurrentWorldAiSpawnConfig(_worldAiSpawnConfig);
	}
}
