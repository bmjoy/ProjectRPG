using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string AbilityName;
    public string Description;
    public DamageType DamageType;
    public int BaseDamage;
    public int ResourceCost;
}
