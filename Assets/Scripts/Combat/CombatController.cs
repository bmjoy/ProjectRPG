using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public delegate void TurnOrderChanged(Character character);
    public TurnOrderChanged OnTurnOrderChanged;

    [SerializeField] private GameObject combatCharacterPrefab;
    [SerializeField] private CombatInterface combatInterface;
    [SerializeField] private Transform enemyPositions;
    [SerializeField] private Transform partyPositions;

    public static CombatController Instance { get; private set; }
    public Character CurrentCharacter { get; private set; }
    public List<Character> EnemyParty { get; private set; }
    public List<Character> HeroParty { get; private set; }
    public List<Character> allCharacters { get; private set; }

    private TurnOrder turnOrder;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        allCharacters = new List<Character>();
        SpawnCharacters();
    }

    private void Start()
    {
        StartCombat();
        Tooltip.Instance.Hide();
    }

    private void SpawnCharacters()
    {
        EnemyParty = new List<Character>();
        HeroParty = new List<Character>();

        var crossDataManager = FindObjectOfType<CrossSceneDataManager>();

        SpawnEnemies(crossDataManager);
        SpawnPlayer(crossDataManager);
    }

    private void SpawnEnemies(CrossSceneDataManager characterData)
    {
        for (int i = 0; i < characterData.EnemyToFight.Enemies.Count; i++)
        {
            var combatChar = Instantiate(combatCharacterPrefab, enemyPositions.GetChild(i).transform.position, Quaternion.identity, combatInterface.transform);
            combatChar.GetComponent<CharacterVisual>().Initialize(characterData.EnemyToFight.Enemies[i]);
            EnemyParty.Add(characterData.EnemyToFight.Enemies[i]);
        }
    }

    private void SpawnPlayer(CrossSceneDataManager characterData)
    {
        for (int i = 0; i < Party.Instance.Members.Count; i++)
        {
            var playerChar = Instantiate(combatCharacterPrefab, partyPositions.GetChild(i).transform.position, Quaternion.identity, combatInterface.transform);
            playerChar.GetComponent<CharacterVisual>().Initialize(Party.Instance.Members[i]);
            HeroParty.Add(Party.Instance.Members[i]);
        }
    }

    private void StartCombat()
    {
        allCharacters = HeroParty.Concat(EnemyParty).ToList();
        turnOrder = new TurnOrder(allCharacters, this);

        foreach (var character in allCharacters.OrderByDescending(character => character.Stats.Dexterity))
        {
            OnTurnOrderChanged += character.CheckIfMyTurn;
            character.Stats.OnDeathCallback += RemoveFromTurnOrder;
        }

        NextTurn();
    }

    public void NextTurn()
    {
        turnOrder.NextTurn();
    }

    private void RemoveFromTurnOrder(Character character)
    {
        OnTurnOrderChanged -= character.CheckIfMyTurn;
        allCharacters.Remove(character);
        turnOrder.Remove(character);

        switch (character)
        {
            case Hero h:
                HeroParty.Remove(h);

                if (HeroParty.Count == 0)
                    Debug.LogError("We died Show some death screen");
                break;

            case Enemy e:
                EnemyParty.Remove(e);

                if(EnemyParty.Count == 0)
                    FindObjectOfType<LootGenerator>().SpawnLoot();
                break;
        }
    }
}
