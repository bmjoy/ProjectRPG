using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler, IItemSlot
{
    public ItemTypes itemType;
    public ItemVisual ItemVisual { get; private set; }
    public Hero currentSelectedHero;
    private Sprite sprite;
    private Inventory inventory;

    public void SetData(Hero selectedHero, ItemTypes slotType, Item equippedItem, Inventory inventory)
    {
        inventory.AddInventorySlot(this);
        currentSelectedHero = selectedHero;
        itemType = slotType;
        ItemVisual = ItemFactory.Instance.GetVisual(equippedItem);
        this.inventory = inventory;

        if (sprite == null)
        {
            sprite = SpriteManager.GetSprite(itemType + "PlaceHolder");
            GetComponent<Image>().sprite = sprite;
        }
    }

    public void UpdateItem(Hero selectedHero, Item equippedItem)
    {
        Clear();
        currentSelectedHero = selectedHero;
        SetItem(equippedItem);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        ItemVisual temp;

        //check if an item gets dropped on us
        if(temp = eventData.pointerDrag.GetComponent<ItemVisual>())
        {
            //If it isn't the right type for this slot we can stop
            if (temp.Item.Type != itemType)
                return;
            else
            {
                //We got the right type now what?
                var otherItemVisual = temp;

                currentSelectedHero.equipmentManager.EquipItem(otherItemVisual.Item, inventory);
                SetItem(otherItemVisual.Item);
            }
        }
    }

    public void Clear()
    {
        if (ItemVisual == null)
            return;

        Destroy(ItemVisual.gameObject);
        ItemVisual = null;
    }

    public void SetItem(Item item)
    {
        ItemVisual = ItemFactory.Instance.GetVisual(item);
        ItemVisual?.SetParent(transform, this);
    }
}
