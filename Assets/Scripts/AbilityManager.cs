using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class AbilityManager
{
    private static List<Ability> abilities;

    public static Ability GetAbility(string abilityName)
    {
        if (abilities == null)
            LoadAbilities();

        var ability = abilities.First(ab => ab.AbilityName == abilityName);
        if (ability != null)
            return ability;
        else
        {
            Debug.LogError($"Couldnt find ability {abilityName}");
            return null;
        }
    }

    private static void LoadAbilities()
    {
        abilities = Resources.LoadAll<Ability>("Abilities").ToList();
    }
}
