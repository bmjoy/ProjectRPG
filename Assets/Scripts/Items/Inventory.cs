using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject ItemVisualPrefab;
    [SerializeField] private InventorySlot inventorySlotsPrefab;
    [SerializeField] private Transform inventorySlotsParent;
    [SerializeField] private int size;

    [SerializeField] private Text moneyText;
    public int Money { get; private set; }

    private List<IItemSlot> inventorySlots;

    void Start()
    {
        inventorySlots = new List<IItemSlot>();

        for (int i = 0; i < size; i++)
        {
            var slot = Instantiate(inventorySlotsPrefab, inventorySlotsParent).GetComponent<InventorySlot>();
            inventorySlots.Add(slot);
            slot.Initialize(this, i);
            slot.gameObject.name = $"ItemSlot {i}";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddItemToInventory(ItemGenerator.CreateNewItem());
        }
    }

    public void SwapItems(Item firstItem, Item secondItem)
    {
        var firstSlot = GetInventorySlotByItem(firstItem);
        var secondSlot = GetInventorySlotByItem(secondItem);

        var temp = firstItem;
        firstSlot.SetItem(secondItem);
        secondSlot.SetItem(temp);
    }

    public void AddItemToInventory(Item itemToAdd, IItemSlot slot = null)
    {
        if (InventoryFull())
        {
            Debug.LogError("Inventory is full when trying to add item to inventory");
            return;
        }

        if(slot == null)
        {
            var emptySlot = GetFirstEmptySlot();
            emptySlot.SetItem(itemToAdd);
        }
        else
        {
            slot.SetItem(itemToAdd);
        }
    }

    public void RemoveItemFromInventory(Item itemToRemove)
    {
        var slot = GetInventorySlotByItem(itemToRemove);
        slot.Clear();
    }

    private IItemSlot GetFirstEmptySlot()
    {
        return inventorySlots.First(slot => slot.ItemVisual == null);
    }

    public IItemSlot GetInventorySlotByItem(Item item)
    {
        return inventorySlots.First(x => x.ItemVisual?.Item == item);
    }

    public void AddInventorySlots(IItemSlot slots)
    {
        inventorySlots.Add(slots);
    }

    public bool InventoryFull()
    {
        for (int i = 0; i < 20; i++)
        {
            if(inventorySlots[i].ItemVisual == null)
                return false;
        }
        return true;
    }

    public void IncreaseMoney(int amount)
    {
        Money += amount;
        moneyText.text = $"{Money}";
    }

    public void DecreaseMoney(int amount)
    {
        Money -= amount;
        moneyText.text = $"{Money}";
    }
}
