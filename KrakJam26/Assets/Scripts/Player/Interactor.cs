using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    public event Action<IInteractable> OnInteractEvent;

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != interactablesLayer) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }

    private void TryInteract()
    {
        if (currentInteractable == null) return;
        Debug.Log("dziala");
        currentInteractable.Interact();
        currentInteractable = null;
    }
}
