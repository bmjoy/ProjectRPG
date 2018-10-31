using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LootItemSlot : MonoBehaviour, IItemSlot, IDropHandler
{
    private Inventory inventory;
    public ItemVisual ItemVisual { get; private set; }

    public void Initialize(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.AddInventorySlot(this);
    }

    //Gets called when an item gets dropped on this slot.
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        ItemVisual otherItem;

        if (otherItem = eventData.pointerDrag.GetComponent<ItemVisual>())
        {
            var invSlot = otherItem.Slot;

            if (ItemVisual == null)
            {
                SetItem(otherItem.Item);
                invSlot.Clear();
            }
            else
                inventory.SwapItems(ItemVisual.Item, otherItem.Item);
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
