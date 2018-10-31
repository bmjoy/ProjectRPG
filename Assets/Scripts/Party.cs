using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public delegate void PartyMemberAdded(Hero heroAdded);
    public PartyMemberAdded OnPartyMemberAdded;

    public delegate void PartyMemberRemoved(Hero heroRemoved);
    public PartyMemberAdded OnPartyMemberRemoved;

    public static Party Instance { get; private set; }
    public List<Hero> Members { get; private set; }
    public bool IsFull => Members.Count >= 5;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Members = new List<Hero>
        {
            CharacterRandomizer.GetHeroByClass(CharacterClass.Mage),
            CharacterRandomizer.GetHeroByClass(CharacterClass.Warrior),
            CharacterRandomizer.GetHeroByClass(CharacterClass.Ranger)
        };
    }

    public void AddPartyMember(Hero hero)
    {
        if(Members.Count < 5)
        {
            Members.Add(hero);
            OnPartyMemberAdded?.Invoke(hero);
        }
    }

    public void RemovePartyMember(Hero hero)
    {
        if (Members.Contains(hero))
        {
            OnPartyMemberRemoved?.Invoke(hero);
            Members.Remove(hero);
        }
    }

    public void HealParty(int percentage)
    {
        foreach (var hero in Members)
        {
            hero.Stats.HealPercent(percentage);
        }
    }
}
