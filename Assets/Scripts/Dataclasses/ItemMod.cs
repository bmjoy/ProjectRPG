using Newtonsoft.Json;
using UnityEngine;

public class ItemMod
{
    public StatTypes Stat;
    public int Value;

    public ItemMod(ItemModJson json)
    {
        Stat = json.Stat;
        Value = Random.Range(json.MinValue, json.MaxValue + 1);
    }
}

