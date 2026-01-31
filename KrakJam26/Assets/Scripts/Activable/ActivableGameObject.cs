using UnityEngine;

public class ActivableGameObject : ActivableBase
{
	[SerializeField]
	private GameObject _targetGameObject;
	public override void Activate()
	{
		_targetGameObject?.SetActive(true);
	}
	public override void Deactivate()
	{
		_targetGameObject?.SetActive(false);
	}
}
