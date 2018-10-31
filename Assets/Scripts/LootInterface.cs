using System;
using System.Collections.Generic;
using UnityEngine;

public class LootInterface : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<Item> loot;
    private Inventory inventory;
    private List<LootItemSlot> slots;

    public void Initialize(List<Item> items)
    {
        slots = new List<LootItemSlot>();
        loot = items;
        inventory = FindObjectOfType<Inventory>();
        SpawnItemSlots();
    }

    private void SpawnItemSlots()
    {
        for (int i = 0; i < loot.Count; i++)
        {
            var newSlot = Instantiate(slotPrefab, slotParent).GetComponent<LootItemSlot>();
            slots.Add(newSlot);

            newSlot.Initialize(inventory);
            newSlot.SetItem(loot[i]);
        }
    }

    private void DestroyItemSlots()
    {
        if (slots == null || slots.Count == 0)
            return;

        foreach (var item in slots)
        {
            Destroy(item.gameObject);
        }

        slots.Clear();
    }

    public void Button_DoneLooting()
    {
        DestroyItemSlots();
        GameInterfaceManager.Instance.CloseAllInterfaces();
        FindObjectOfType<CrossSceneDataManager>().EndCombat(true);
    }
}
