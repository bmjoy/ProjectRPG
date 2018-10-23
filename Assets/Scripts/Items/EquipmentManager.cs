using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EquipmentManager
{
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    private Dictionary<ItemTypes, Item> equippedItems;
    private StatsManager statsManager;

    public EquipmentManager(StatsManager statsManager)
    {
        this.statsManager = statsManager;
        equippedItems = new Dictionary<ItemTypes, Item>();
    }

    public void EquipItem(Item itemToEquip, Inventory inventory)
    {
        var copy = itemToEquip;

        //IF we have an item of that type equipped, Unequip that first.
        if (equippedItems.ContainsKey(itemToEquip.Type) && equippedItems[itemToEquip.Type] != null)
            UnequipItem(equippedItems[copy.Type], inventory);

        //Remove it from the inventory and clear the slot
        inventory.RemoveItemFromInventory(itemToEquip);

        //Equip item and add stats
        equippedItems[copy.Type] = copy;
        foreach (var mod in copy.ItemMods)
        {
            statsManager.IncreaseStat(mod.Stat, mod.Value);
        }

        OnItemChangedCallback?.Invoke();

        Debug.Log($"Equipped {copy.Type}");
    }

    public void UnequipItem(Item itemToUnequip, Inventory inventory, IItemSlot slotToUnequipTo = null)
    {
        if (!equippedItems.ContainsValue(itemToUnequip))
            return;
        else
        {
            var copy = itemToUnequip;
            inventory.GetInventorySlotByItem(itemToUnequip).Clear();

            //Deduct stats
            foreach (var mod in copy.ItemMods)
            {
                statsManager.DecreaseStat(mod.Stat, mod.Value);
            }            

            //Add the item to the inventory
            //TODO: What if inventory is full??
            inventory.AddItemToInventory(equippedItems[copy.Type], slotToUnequipTo);
            equippedItems[copy.Type] = null;

            OnItemChangedCallback?.Invoke();
            Debug.Log($"Unequipped {copy.Type}");
        }
    }

    public Item GetEquippedItem(ItemTypes type)
    {
        if (!equippedItems.ContainsKey(type))
            return null;
        return equippedItems[type];
    }
}

