using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IItemSlot
{
    private Inventory inventory;
    private int index;
    public ItemVisual ItemVisual { get; private set; }

    public void Initialize(Inventory inventory, int index)
    {
        this.inventory = inventory;
        this.index = index;
    }

    //Gets called when an item gets dropped on this slot.
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        ItemVisual otherItem;

        if (otherItem = eventData.pointerDrag.GetComponent<ItemVisual>())
        {
            switch (otherItem.Slot)
            {
                case EquipmentSlot eq:
                        var eqSlot = (EquipmentSlot)otherItem.Slot;
                        if (ItemVisual == null)
                            eqSlot.currentSelectedHero.equipmentManager.UnequipItem(otherItem.Item, inventory, this);
                        else if (ItemVisual.Item.Type != otherItem.Item.Type)
                            return;
                        else
                            eqSlot.currentSelectedHero.equipmentManager.EquipItem(ItemVisual.Item, inventory);
                    break;

                case InventorySlot inv:
                        var invSlot = otherItem.Slot;

                        if (ItemVisual == null)
                        {
                            SetItem(otherItem.Item);
                            invSlot.Clear();
                        }
                        else
                            inventory.SwapItems(ItemVisual.Item, otherItem.Item);
                    break;

                case LootItemSlot loot:
                    var lootSlot = otherItem.Slot;

                    if (ItemVisual == null)
                    {
                        SetItem(otherItem.Item);
                        lootSlot.Clear();
                    }
                    else
                        inventory.SwapItems(ItemVisual.Item, otherItem.Item);
                    break;

                case ShopItemSlot shop:
                        var shopSlot = (ShopItemSlot)otherItem.Slot;
                        shopSlot.shop.BuyItem(otherItem.Item);
                    break;
            }
        }
    }

    public void SetItem(Item item)
    {
        Clear();
        ItemVisual = ItemFactory.Instance.GetVisual(item);
        ItemVisual.SetParent(transform, this);
    }

    public void Clear()
    {
        if (ItemVisual == null)
            return;

        Destroy(ItemVisual?.gameObject);
        ItemVisual = null;
    }
}
