using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public GameObject ItemVisualPrefab;

    public static ItemFactory Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public ItemVisual GetVisual(Item item)
    {
        if (item == null)
            return null;

        var visual = Instantiate(ItemVisualPrefab).GetComponent<ItemVisual>();

        visual.Item = item;

        return visual;
    }
}
