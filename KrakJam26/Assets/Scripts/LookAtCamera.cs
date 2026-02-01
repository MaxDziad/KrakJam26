using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private void Update()
	{
		if (CameraSystem.Instance.MainCamera != null)
		{
			transform.LookAt(CameraSystem.Instance.MainCamera.transform);
		}
	}
}
