using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public StatsManager Stats { get; private set; }
    public bool IsMyTurn { get; protected set; }
    public string Name { get; protected set; }
    public Sprite Sprite { get; protected set; }
    public List<Ability> Abilities { get; protected set; }
    public CharacterVisual Visual;
    public bool IsHero => GetType() == typeof(Hero);

    public Character(StatsDictionary stats)
    {
        Stats = new StatsManager(stats, this);
        Abilities = new List<Ability>();
        stats.abilities.ToList().ForEach(ab => Abilities.Add(AbilityManager.GetAbility(ab)));
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

    public void Attack(Ability ability, Character target)
    {
        switch (ability.DamageType)
        {
            case DamageType.Physical:
                target.TakeDamage(Stats.CalculateDamage(ability));
                CombatController.Instance.NextTurn();
                break;

            case var expression when ability.DamageType == DamageType.Magic || ability.DamageType == DamageType.Ranged:
                var abil = (RangedAttack)ability;
                ProjectileFactory.Instance.SpawnProjectile(abil.projectileSprite, Stats.CalculateDamage(abil), this, target);
                break;
        }
    }
}
