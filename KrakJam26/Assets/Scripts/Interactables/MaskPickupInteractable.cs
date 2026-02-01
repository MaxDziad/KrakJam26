using System;
using UnityEngine;

public class MaskPickupItem : MonoBehaviour, IInteractable
{
	[SerializeField]
	private MaskType maskType;

	[SerializeField]
	private ActivableBase _activableOnPickup;

	public event Action OnDisabledEvent;

	public string GetPromptText()
	{
		return "Pick up ???";
	}

	public void Interact()
	{
		MaskSystemManager.Instance.UnlockMask(maskType);

		if (_activableOnPickup != null)
		{
			_activableOnPickup.Activate();
		}

		Destroy(gameObject);
	}

	private void OnDisable()
	{
		OnDisabledEvent?.Invoke();
	}
}
