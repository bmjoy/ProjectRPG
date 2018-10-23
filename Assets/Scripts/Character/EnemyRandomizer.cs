using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public static class EnemyRandomizer
{
    private static List<EnemyPreset> enemyPresets;

    public static Enemy GetRandomEnemy()
    {
        if(enemyPresets == null)
        {
            var json = Resources.Load<TextAsset>("Data/EnemyPresets");
            enemyPresets = JsonConvert.DeserializeObject<List<EnemyPreset>>(json.text);
        }

        return new Enemy(enemyPresets.GetRandom());
    }

    public static List<Enemy> GetRandomEnemies(int numberOfEnemies)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemies.Add(GetRandomEnemy());
        }

        return enemies;
    }
}