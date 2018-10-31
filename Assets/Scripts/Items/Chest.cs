using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private List<Item> items;

    private void OnMouseDown()
    {
        GameInterfaceManager.Instance.ShowLootWindow(items);
    }

    public void AddItem(Item item)
    {
        if (items == null)
            items = new List<Item>();

        items.Add(item);
    } 
}
