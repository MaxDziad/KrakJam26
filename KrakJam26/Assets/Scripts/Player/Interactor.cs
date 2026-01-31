using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	public event Action<IInteractable> OnInteractEvent;
	public event Action<IInteractable> OnInteractableDetectedEvent;
	public event Action<IInteractable> OnInteractableLostEvent;

	[SerializeField]
	private int interactablesLayer;

	private IInteractable currentInteractable;

	[SerializeField]
	private PlayerController playerController;

	private void OnEnable()
	{
		playerController.OnInteractEvent += TryInteract;
	}

	private void OnDisable()
	{
		playerController.OnInteractEvent -= TryInteract;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != interactablesLayer) return;

		IInteractable interactable = other.GetComponent<IInteractable>();
		if (interactable != null)
		{
			currentInteractable = interactable;
			currentInteractable.OnDisabledEvent += OnInteractableDisabled;
			OnInteractableDetectedEvent?.Invoke(currentInteractable);
		}
	}

	private void OnInteractableDisabled()
	{
		currentInteractable.OnDisabledEvent -= OnInteractableDisabled;
		OnInteractableLostEvent?.Invoke(currentInteractable);
		currentInteractable = null;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer != interactablesLayer) return;

		IInteractable interactable = other.GetComponent<IInteractable>();
		if (interactable != null && currentInteractable == interactable)
		{
			currentInteractable.OnDisabledEvent -= OnInteractableDisabled;
			currentInteractable = null;
			OnInteractableLostEvent?.Invoke(interactable);
		}
	}

	private void TryInteract()
	{
		if (currentInteractable == null) return;
		currentInteractable.Interact();
		currentInteractable = null;
	}
}
