using System;
using UnityEngine;

public class InteractableExample : MonoBehaviour, IInteractable
{
	public event Action OnDisabledEvent;

	public string GetPromptText()
	{
		return "gowno";
	}

	public void Interact()
	{
		Debug.Log("interacted");
		Destroy(gameObject);
	}

	private void OnDisable()
	{
		OnDisabledEvent?.Invoke();
	}
}
