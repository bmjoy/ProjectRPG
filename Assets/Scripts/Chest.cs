using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private List<Item> items;

    private void OnMouseDown()
    {
        var inventory = FindObjectOfType<Inventory>();
        for (int i = 0; i < items.Count; i++)
        {
            inventory.AddItemToInventory(items[i]);
        }
        CrossSceneDataManager.Instance.EndCombat(true);
    }

    public void AddItem(Item item)
    {
        if (items == null)
            items = new List<Item>();

        items.Add(item);
    } 
}
