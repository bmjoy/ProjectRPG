using Newtonsoft.Json;
using UnityEngine;

public class ItemMod
{
    public StatTypes Stat;
    public string Name;
    public int Value;

    public ItemMod(ItemModJson json)
    {
        Stat = json.Stat;
        Name = json.Name;
        Value = Random.Range(json.MinValue, json.MaxValue + 1);
    }
}

