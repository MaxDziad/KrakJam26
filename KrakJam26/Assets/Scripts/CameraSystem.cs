using UnityEngine;

public class CameraSystem : MonoBehaviour
{
	public static CameraSystem Instance { get; private set; }

	private Camera _mainCamera;
	public Camera MainCamera => _mainCamera;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		_mainCamera = GetComponent<Camera>();
	}
}
