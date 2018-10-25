using System;
using System.Collections.Generic;

public class StatsManager
{
    public delegate void OnStatChanged();
    public OnStatChanged OnStatChangedCallback;

    public delegate void OnHealthChanged();
    public OnHealthChanged OnHealthChangedCallback;

    public delegate void OnDeath(Character character);
    public OnDeath OnDeathCallback;

    public Dictionary<StatTypes, Stat> statValues { get; private set; }
    private Character character;

    public StatsManager(StatsDictionary preset, Character character)
    {
        statValues = new Dictionary<StatTypes, Stat>();
        this.character = character;

        foreach (var item in preset.Stats)
        {
            statValues[item.Key] = new Stat(item.Value, item.Value);
        }

        //Set max hp for hero alot higher than for enemies
        //TODO: make better
        statValues[StatTypes.Health] = new Stat((character.IsHero ? 50 : 10) + (Stamina * 4), 0);
        statValues[StatTypes.Health].CurrentValue = GetBaseStatValue(StatTypes.Health);
    }

    public int Armor => GetCurrentStatValue(StatTypes.Armor);
    public int CritChance => GetCurrentStatValue(StatTypes.CritChance);
    public int CurrentHealth => GetCurrentStatValue(StatTypes.Health);
    public int Dexterity => GetCurrentStatValue(StatTypes.Dexterity);
    public int Haste => GetCurrentStatValue(StatTypes.Haste);
    public int Intelligence => GetCurrentStatValue(StatTypes.Intelligence);
    public int Stamina => GetCurrentStatValue(StatTypes.Stamina);
    public int Strength => GetCurrentStatValue(StatTypes.Strength);
    public int MaxHealth => GetBaseStatValue(StatTypes.Health);

    public int GetBaseStatValue(StatTypes statType)
    {
        return statValues[statType].BaseValue;
    }

    public int GetCurrentStatValue(StatTypes statType)
    {
        switch (statType)
        {
            case StatTypes.CritChance:
                return statValues[StatTypes.CritChance].CurrentValue + (Dexterity / 4);
            case StatTypes.Haste:
                return statValues[StatTypes.Haste].CurrentValue + (Intelligence / 4);
            default:
                return statValues[statType].CurrentValue;
        }
    }

    public void SetBaseStatValue(StatTypes statType, int value)
    {
        statValues[statType].BaseValue = value;
        OnStatChangedCallback?.Invoke();
    }

    public void IncreaseStat(StatTypes statType, int value)
    {
        if (statType == StatTypes.Stamina)
            IncreaseMaxHealth(value * 4);

        statValues[statType].CurrentValue += value;
    }

    public void DecreaseStat(StatTypes statType, int value)
    {
        if (statType == StatTypes.Stamina)
            IncreaseMaxHealth(-value * 4);

        statValues[statType].CurrentValue -= value;
    }

    private void IncreaseMaxHealth(int value)
    {
        statValues[StatTypes.Health].BaseValue += value;
        statValues[StatTypes.Health].CurrentValue += value;
        OnHealthChangedCallback?.Invoke();
    }

    public DamageData CalculateDamage(Ability ability)
    {
        var baseDmg = GetBaseDamage(ability);
        var dmgRange = (int)UnityEngine.Random.Range(baseDmg * 0.8f, baseDmg);

        var randomNumb = UnityEngine.Random.Range(0,100);

        //Check if we crit
        if (randomNumb <= GetCurrentStatValue(StatTypes.CritChance))
            return new DamageData(dmgRange * 2, true);
        else
            return new DamageData(dmgRange, false);
    }

    private int GetBaseDamage(Ability ability)
    {
        int baseDmg = ability.BaseDamage;
        switch (ability.DamageType)
        {
            case DamageType.Magic:
                baseDmg += Intelligence;
                break;
            case DamageType.Physical:
                baseDmg += GetCurrentStatValue(StatTypes.Damage) + Strength;
                break;
            case DamageType.Ranged:
                baseDmg += GetCurrentStatValue(StatTypes.Damage) + Dexterity;
                break;
        }
        return baseDmg;
    }

    public void TakeDamage(DamageData dmgData)
    {
        statValues[StatTypes.Health].CurrentValue -= dmgData.Damage - (Armor / 8);

        CombatText.DisplayDamageText(dmgData, character);

        OnHealthChangedCallback?.Invoke();

        if (CurrentHealth <= 0)
        {
            //TODO: Hero is invincible 
            if (character.IsHero)
            {
                statValues[StatTypes.Health].CurrentValue = GetBaseStatValue(StatTypes.Health);
                OnHealthChangedCallback?.Invoke();
            }
            else
            {
                OnDeathCallback?.Invoke(character);
                character.Visual.Destroy();
            }
        }
    }
}