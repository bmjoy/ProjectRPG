﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : Character
{
    private float moveTimer;

    public Enemy(EnemyPreset preset) : base(preset)
    {
        Name = $"Enemy {Random.Range(0,110)}";
        Sprite = SpriteManager.GetSprite(preset.SpriteName);
    }

    private Character GetEnemyTarget()
    {
        var party = CombatController.Instance.HeroParty;
        return party[Random.Range(0, party.Count)];
    }

    public void DecideMove()
    {
        if (IsMyTurn)
        {
            var target = GetEnemyTarget();
            var ability = Abilities.GetRandom();
            Attack(ability, target);
            IsMyTurn = false;
        }
    }

    public override void Update()
    {
        if (IsMyTurn)
        {
            moveTimer -= Time.deltaTime;
            if(moveTimer <=0)
                DecideMove();
        }
    }

    public override void CheckIfMyTurn(Character character)
    {
        base.CheckIfMyTurn(character);
        moveTimer = Random.Range(1.5f, 2f);
    }
}
