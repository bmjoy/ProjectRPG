using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IItemSlot, IDropHandler, IPointerClickHandler
{
    public ItemVisual ItemVisual { get; private set; }
    public Shop shop { get; private set; }

    public void Initialize(Shop shop, Item item)
    {
        this.shop = shop;
        SetItem(item);
        item.Price *= 5;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemVisual otherItem;
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (otherItem = eventData.pointerDrag.GetComponent<ItemVisual>())
        {
            shop.SellItem(otherItem.Item);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return;

        shop.BuyItem(ItemVisual.Item);
    }
}
