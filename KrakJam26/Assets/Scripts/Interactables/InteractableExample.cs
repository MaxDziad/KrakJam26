using UnityEngine;

public class InteractableExample : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("interacted");
        Destroy(gameObject);
    }
}
