using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

//TODO: this doesn't take care of just data anymore.
public class CrossSceneDataManager : MonoBehaviour
{
    public static CrossSceneDataManager Instance { get; private set; }

    public List<EnemyData> EnemyList { get; private set; }
    public EnemyData EnemyToFight { get; private set; }
    public List<Hero> Party { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);

        EnemyList = new List<EnemyData>();
        Party = new List<Hero>
        {
            CharacterRandomizer.GetRandomHero(),
            CharacterRandomizer.GetRandomHero()
        };

        for (int i = 0; i < Random.Range(0,3); i++)
        {
            Party.Add(CharacterRandomizer.GetRandomHero());
        }
    }

    private void Start()
    {
        CollectOverworldData();
    }

    public void CollectOverworldData()
    {
        EnemyList.Clear();

        foreach (var enemy in FindObjectsOfType<OverworldEnemy>())
        {
            EnemyList.Add(new EnemyData(enemy.transform.position, enemy.enemies));
        }
    }

    public void StartCombat(OverworldEnemy enemy)
    {
        CollectOverworldData();

        var index = EnemyList.FindIndex(item => item.Enemies == enemy.enemies);
        EnemyToFight = EnemyList[index];

        SceneManager.LoadScene("CombatScene");
    }

    public void EndCombat(bool enemyDefeated)
    {
        if (enemyDefeated)
            EnemyList.Remove(EnemyToFight);

        EnemyToFight = null;

        SceneManager.LoadScene("OverworldScene");
    }
}
