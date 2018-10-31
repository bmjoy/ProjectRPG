using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Item : ITooltip
{
    public ItemTypes Type { get; private set; }
    public List<ItemMod> ItemMods { get; private set; }
    public Sprite Sprite { get; private set; }
    public int Price;

    public Item(List<ItemMod> itemMods, ItemTypes type, Sprite sprite)
    {
        ItemMods = itemMods;
        Type = type;
        Sprite = sprite;
        Price = UnityEngine.Random.Range(5, 10) + ItemMods.Count * 5;
    }

    public string TooltipText => $"{Type}\n {GetItemModString()}\nPrice:{Price}";

    private string GetItemModString()
    {
        string text = "";

        foreach (var item in ItemMods)
        {
            text += $"+{item.Value} {item.Stat} \n";
        }

        return text;
    }
}

