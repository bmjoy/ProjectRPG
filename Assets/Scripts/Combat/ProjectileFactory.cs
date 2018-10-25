using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public static ProjectileFactory Instance { get; private set; }

    public GameObject projectilePrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    public void SpawnProjectile(Sprite abilitySprite, DamageData dmg, Character caster, Character target)
    {
        var projectile = Instantiate(projectilePrefab, caster.Visual.Position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Initialize(abilitySprite, dmg, target.Visual);
    }
}
