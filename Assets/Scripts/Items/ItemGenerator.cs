using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public static class ItemGenerator
{
    private static List<ItemModJson> itemPrefixes;
    private static List<ItemModJson> itemSuffixes;

    public static List<ItemMod> GetItemMods(ItemTypes type)
    {
        if (itemPrefixes == null || itemSuffixes == null)
        {
            LoadData();
        }

        var itemMods = new List<ItemMod>();


        itemMods.AddRange(GetRandomPrefixes(UnityEngine.Random.Range(1,2 + 1)));
        itemMods.AddRange(GetRandomSuffixes(1));

        //Add armor for armors and damage for weapons.
        switch (type)
        {
            case var expression when (type == ItemTypes.Armor || type == ItemTypes.Boots || type == ItemTypes.Gloves || type == ItemTypes.Helmet || type == ItemTypes.Shield):
                if(itemMods.Any(mod => mod.Stat == StatTypes.Armor))
                {
                    var armor = GetSuffixByStat(StatTypes.Armor);
                    itemMods.First(mod => mod.Stat == StatTypes.Armor).Value += armor.Value;
                }
                else
                {
                    itemMods.Add(GetSuffixByStat(StatTypes.Armor));
                }
                break;
            case ItemTypes.Weapon:
                if (itemMods.Any(mod => mod.Stat == StatTypes.Damage))
                {
                    var weapon = GetSuffixByStat(StatTypes.Damage);
                    itemMods.First(mod => mod.Stat == StatTypes.Damage).Value += weapon.Value;
                }
                else
                {
                    itemMods.Add(GetSuffixByStat(StatTypes.Damage));
                }
                break;
        }

        return itemMods;
    }

    private static void LoadData()
    {
        itemPrefixes = JsonConvert.DeserializeObject<List<ItemModJson>>(Resources.Load<TextAsset>("Data/ItemPrefixes").text);
        itemSuffixes = JsonConvert.DeserializeObject<List<ItemModJson>>(Resources.Load<TextAsset>("Data/ItemSuffixes").text);
    }

    public static Item CreateNewItem()
    {
        var itemType = (ItemTypes)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemTypes)).Length);
        var sprite = SpriteManager.GetSprite(itemType.ToString());

        return new Item(GetItemMods(itemType), itemType, sprite);
    }

    private static List<ItemMod> GetRandomPrefixes(int amount)
    {
        List<ItemMod> list = new List<ItemMod>();
        for (int i = 0; i < amount; i++)
        {
            var randomPrefix = new ItemMod(itemPrefixes.GetRandom());

            if (list.Any(mod => mod.Stat == randomPrefix.Stat))
                list.First(mod => mod.Stat == randomPrefix.Stat).Value += randomPrefix.Value;
            else
                list.Add(randomPrefix);

        }
        return list;
    }

    private static List<ItemMod> GetRandomSuffixes(int amount)
    {
        List<ItemMod> list = new List<ItemMod>();
        for (int i = 0; i < amount; i++)
        {
            var randomSuffix = new ItemMod(itemSuffixes.GetRandom());

            if (list.Any(mod => mod.Stat == randomSuffix.Stat))
                list.First(mod => mod.Stat == randomSuffix.Stat).Value += randomSuffix.Value;
            else
                list.Add(randomSuffix);
        }
        return list;
    }

    private static ItemMod GetPrefixByStat(StatTypes statType)
    {
        return new ItemMod(itemPrefixes.Where(mod => mod.Stat == statType).ToArray().GetRandom());
    }

    private static ItemMod GetSuffixByStat(StatTypes statType)
    {
        return new ItemMod(itemSuffixes.Where(mod => mod.Stat == statType).ToArray().GetRandom());
    }
}
