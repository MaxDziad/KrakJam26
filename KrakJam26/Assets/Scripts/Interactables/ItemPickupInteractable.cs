using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] 
    private ItemType itemType;

    private InventoryManager _inventory;

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
}
