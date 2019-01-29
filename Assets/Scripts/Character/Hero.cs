using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class Hero : Character
{
    public CharacterClass Class { get; private set; }
    public EquipmentManager equipmentManager { get; private set; }

    public Hero(CharacterClassPreset preset) : base(preset)
    {
        Class = preset.characterClass;
        Sprite = SpriteManager.GetSprite(Class.ToString());
        Name = $"Hero {UnityEngine.Random.Range(0, 10)}";
        equipmentManager = new EquipmentManager(Stats);
    }
}