using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] 
    private ItemType itemType;

    private InventoryManager _inventory;

	public event Action OnDisabledEvent;

	private void Start()
    {
        _inventory = FindObjectOfType<InventoryManager>();
    }

    public void Interact()
    {
        if (_inventory == null) return;

        if (_inventory.PickUpItem(itemType))
        {
            gameObject.SetActive(false);
        }
    }

	public string GetPromptText()
	{
        return "Pick up part";
	}

	private void OnDisable()
	{
		OnDisabledEvent?.Invoke();
	}
}
