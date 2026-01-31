using UnityEngine;

public class AiAgentSystemManager : MonoBehaviour
{
	public static AiAgentSystemManager Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}
