using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private Transform chestPosition;

    public void SpawnLoot()
    {
        var enemies = FindObjectOfType<CrossSceneDataManager>().EnemyToFight;
        int amountOfLoot = 1 + (enemies.Enemies.Count / 2);
        var inventory = FindObjectOfType<Inventory>();
        var chest = Instantiate(chestPrefab, chestPosition.position, Quaternion.identity).GetComponent<Chest>();

        for (int i = 0; i < amountOfLoot; i++)
        {
            chest.AddItem(ItemGenerator.CreateNewItem());
        }
    }
}
