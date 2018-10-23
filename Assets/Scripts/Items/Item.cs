using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Item : ITooltip
{
    public ItemTypes Type;
    public List<ItemMod> ItemMods;
    public Sprite Sprite;

    public Item(List<ItemMod> itemMods, ItemTypes type, Sprite sprite)
    {
        ItemMods = itemMods;
        Type = type;
        Sprite = sprite;
    }

    public string TooltipText => $"{Type}\n" + GetItemModString();

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

