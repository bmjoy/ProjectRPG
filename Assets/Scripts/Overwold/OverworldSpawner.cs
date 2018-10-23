using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OverworldSpawner : MonoBehaviour
{
    public GameObject OverWorldEnemyPrefab;
    private List<OverworldEnemy> allEnemies;
    private List<EnemyData> listOfEnemies;

    private void Start()
    {
        listOfEnemies = FindObjectOfType<CrossSceneDataManager>().EnemyList;
        allEnemies = new List<OverworldEnemy>();
        SpawnEnemies();

        InvokeRepeating(nameof(SpawnNewEnemy), 5, 5);
    }

    private void SpawnEnemies()
    {
        if(listOfEnemies.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnNewEnemy();
            }
        }
        else
        {
            foreach (var enemy in listOfEnemies)
            {
                var overworldEnemy = Instantiate(OverWorldEnemyPrefab).GetComponent<OverworldEnemy>();
                overworldEnemy.enemies = enemy.Enemies;
                overworldEnemy.transform.position = enemy.Position;
                allEnemies.Add(overworldEnemy);
            }
        }
    }

    private void SpawnNewEnemy()
    {
        var newEnemy = Instantiate(OverWorldEnemyPrefab).GetComponent<OverworldEnemy>();
        newEnemy.enemies = GetRandomEnemies(1, 5);
        newEnemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        allEnemies.Add(newEnemy);
    }

    private List<Enemy> GetRandomEnemies(int min, int max)
    {
        return EnemyRandomizer.GetRandomEnemies(Random.Range(min, max));
    }
}

