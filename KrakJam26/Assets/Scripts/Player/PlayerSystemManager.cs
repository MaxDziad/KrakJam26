using UnityEngine;

public class PlayerSystemManager : MonoBehaviour
{
	public static PlayerSystemManager Instance { get; private set; }

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

	public PlayerController PlayerController { get; private set; }

	public void RegisterSelf(PlayerController playerController)
	{
		PlayerController = playerController;
	}
}
