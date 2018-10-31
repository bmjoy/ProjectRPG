using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private Transform shopSlotsParent;

    private Inventory inventory;
    private ShopInventory shopInventory;
    private List<ShopItemSlot> itemSlots;
    private List<GameObject> slotVisuals;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void Initialize(ShopInventory shopInventory)
    {
        if (itemSlots == null)
        {
            itemSlots = new List<ShopItemSlot>();
            slotVisuals = new List<GameObject>();
        }

        ClearShop();

        for (int i = 0; i < shopInventory.AvailableItems.Count; i++)
        {
            var newSlot = Instantiate(shopSlotPrefab, shopSlotsParent);
            slotVisuals.Add(newSlot);

            itemSlots.Add(newSlot.GetComponent<ShopItemSlot>());
            itemSlots[i].Initialize(this, shopInventory.AvailableItems[i]);
        }
    }

    private void ClearShop()
    {
        for (int i = 0; i < slotVisuals.Count; i++)
        {
            Destroy(slotVisuals[i]);
        }
        slotVisuals.Clear();

        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].Clear();
        }
        itemSlots.Clear();
    }

    public void SellItem(Item item)
    {
        inventory.IncreaseMoney(item.Price);
        inventory.RemoveItemFromInventory(item);
    }

    public void BuyItem(Item item)
    {
        if (!inventory.InventoryFull())
        {
            if(inventory.DecreaseMoney(item.Price))
                inventory.AddItemToInventory(item);
        }
    }
}
