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

    private Queue<Character> turnOrder;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        turnOrder = new Queue<Character>();
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
        for (int i = 0; i < characterData.Party.Count; i++)
        {
            var playerChar = Instantiate(combatCharacterPrefab, partyPositions.GetChild(i).transform.position, Quaternion.identity, combatInterface.transform);
            playerChar.GetComponent<CharacterVisual>().Initialize(characterData.Party[i]);
            HeroParty.Add(characterData.Party[i]);
        }
    }

    private void StartCombat()
    {
        allCharacters = HeroParty.Concat(EnemyParty).ToList();
        foreach (var character in allCharacters.OrderByDescending(character => character.Stats.Dexterity))
        {
            turnOrder.Enqueue(character);
            OnTurnOrderChanged += character.CheckIfMyTurn;
            character.Stats.OnDeathCallback += RemoveFromTurnOrder;
        }

        NextTurn();
    }

    public void NextTurn()
    {
        if(allCharacters.Contains(turnOrder.Peek()))
            CurrentCharacter = turnOrder.Dequeue();
        else
        {
            turnOrder.Dequeue();
            CurrentCharacter = turnOrder.Dequeue();
        }

        OnTurnOrderChanged?.Invoke(CurrentCharacter);

        turnOrder.Enqueue(CurrentCharacter);
    }

    private void RemoveFromTurnOrder(Character character)
    {
        switch (character)
        {
            case Hero h:
                //TODO: Hero is invincible atm.
                break;
            case Enemy e:
                allCharacters.Remove(character);
                EnemyParty.Remove(e);

                if(EnemyParty.Count == 0)
                    FindObjectOfType<LootGenerator>().SpawnLoot();
                break;
        }
    }
}
