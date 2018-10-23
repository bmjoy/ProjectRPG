using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public Vector3 Position { get; private set; }
    public List<Enemy> Enemies { get; private set; }

    public EnemyData(Vector3 position, List<Enemy> enemies)
    {
        Position = position;
        Enemies = enemies;
    }
}

