using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class CharacterRandomizer
{
    public static CharacterClass GetRandomClass()
    {
        return (CharacterClass)UnityEngine.Random.Range(0, Enum.GetNames(typeof(CharacterClass)).Length);
    }

    public static CharacterClassPreset GetClassPreset(CharacterClass charClass)
    {
        var json = Resources.Load<TextAsset>("Data/ClassPresets");
        var presets = JsonConvert.DeserializeObject<List<CharacterClassPreset>>(json.text);
        return presets.FirstOrDefault(x => x.characterClass == charClass);
    }

    public static Hero GetRandomHero()
    {
        return new Hero(GetClassPreset(GetRandomClass()));
    }

    public static Hero GetHeroByClass(CharacterClass charclass)
    {
        return new Hero(GetClassPreset(charclass));
    }
}