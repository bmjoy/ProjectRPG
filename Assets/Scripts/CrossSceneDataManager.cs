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
    public Party Party { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);

        EnemyList = new List<EnemyData>();
        Party = Party.Instance;
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

        GameInterfaceManager.Instance.CloseAllInterfaces();
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
