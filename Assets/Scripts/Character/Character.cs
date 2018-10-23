using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public StatsManager Stats { get; private set; }
    public bool IsMyTurn { get; private set; }
    public string Name { get; protected set; }
    public Sprite Sprite { get; protected set; }
    public CharacterVisual Visual;
    public bool IsHero => GetType() == typeof(Hero);

    public Character(StatsDictionary stats)
    {
        Stats = new StatsManager(stats, this);
    }

    public virtual void Update() {}

    public virtual void CheckIfMyTurn(Character character)
    {
        if (character == this)
            IsMyTurn = true;
        else IsMyTurn = false;
    }

    public void TakeDamage(DamageData damageData)
    {
        Stats.TakeDamage(damageData);
    }
}
