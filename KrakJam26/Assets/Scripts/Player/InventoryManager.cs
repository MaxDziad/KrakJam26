using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public event Action<ItemType> OnItemChanged;

    [SerializeField] private ItemType currentItem = ItemType.EmptyHand;

    [SerializeField] private GameObject itemAObject;
    [SerializeField] private GameObject itemBObject;
    [SerializeField] private GameObject itemCObject;

    public ItemType CurrentItem => currentItem;

    private void Awake()
    {
        UpdateUI();
    }

    public bool PickUpItem(ItemType newItem)
    {
        if (currentItem != ItemType.EmptyHand)
            return false;

        currentItem = newItem;
        OnItemChanged?.Invoke(currentItem);
        UpdateUI();
        return true;
    }

    public void UseItem()
    {
        if (currentItem == ItemType.EmptyHand)
            return;

        currentItem = ItemType.EmptyHand;
        OnItemChanged?.Invoke(currentItem);
        UpdateUI();
    }

    public bool IsHolding(ItemType itemToCheck)
    {
        return currentItem == itemToCheck;
    }

    private void UpdateUI()
    {
        if (itemAObject != null) itemAObject.SetActive(currentItem == ItemType.ItemA);
        if (itemBObject != null) itemBObject.SetActive(currentItem == ItemType.ItemB);
        if (itemCObject != null) itemCObject.SetActive(currentItem == ItemType.ItemC);
    }
}
