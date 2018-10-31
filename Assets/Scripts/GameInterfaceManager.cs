using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameInterfaceManager : MonoBehaviour
{
    public static GameInterfaceManager Instance { get; private set; }

    [SerializeField] private GameObject characterSheet;
    [SerializeField] private GameObject shopInterface;
    [SerializeField] private GameObject hireHeroInterface;
    [SerializeField] private GameObject dialogueInterface;
    [SerializeField] private GameObject lootInterface;

    private Inventory inventory;

    private Dictionary<GameInterface, GameObject> interfaces;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        interfaces = new Dictionary<GameInterface, GameObject>()
        {
            { GameInterface.CharacterSheet, characterSheet },
            { GameInterface.Shop, shopInterface },
            { GameInterface.HireHero, hireHeroInterface },
            { GameInterface.Dialogue, dialogueInterface },
            { GameInterface.Loot, lootInterface }
        };

        //Set inventory initial parent so we can always return it to it's original position.
        inventory.CharacterSheet = interfaces[GameInterface.CharacterSheet];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.C))
            OpenInterface(GameInterface.CharacterSheet);

        if (Input.GetKeyDown(KeyCode.Escape) && !IsInterfaceOpen(GameInterface.Loot))
            CloseAllInterfaces();
    }

    public void OpenInterface(GameInterface interfaceToOpen)
    {
        if (!interfaces.ContainsKey(interfaceToOpen))
        {
            Debug.LogError($"Trying to open {interfaceToOpen} but it doesn't exist");
            return;
        }

        OpenInterfaceCloseRest(interfaceToOpen);
    }

    public void CloseAllInterfaces()
    {
        foreach (var inter in interfaces)
        {
            if (inter.Key == GameInterface.Shop)
                inventory.ResetParent();

            inter.Value.SetActive(false);
        }
        Tooltip.Instance.Hide();
    }

    private void OpenInterfaceCloseRest(GameInterface gameInterface)
    {
        foreach (var panel in interfaces)
        {
            if (panel.Key != gameInterface)
                panel.Value.SetActive(false);
            else
                panel.Value.SetActive(!panel.Value.activeSelf);
        }
        Tooltip.Instance.Hide();
    }

    public void OpenInventoryUnder(Transform parent)
    {
        inventory.SetTempParent(parent);
    }

    public void ResetInventoryPosition()
    {
        inventory.ResetParent();
    }

    public void OpenShopPanel(ShopInventory shopToOpen)
    {
        OpenInterfaceCloseRest(GameInterface.Shop);
        OpenInventoryUnder(shopInterface.transform.GetChild(0));
        shopInterface.GetComponent<Shop>().Initialize(shopToOpen);
    }

    public void OpenDialogue(Dictionary<string, DialogueOption> options)
    {
        CloseAllInterfaces();
        OpenInterfaceCloseRest(GameInterface.Dialogue);
        DialogueManager.Instance.ShowDialogue(options);
    }

    public void OpenHireHeroInterface(List<Hero> heroes)
    {
        CloseAllInterfaces();
        OpenInterfaceCloseRest(GameInterface.HireHero);
        hireHeroInterface.GetComponent<NewHeroInterface>().DrawButtons(heroes);
    }

    public void ShowLootWindow(List<Item> items)
    {
        CloseAllInterfaces();
        OpenInterfaceCloseRest(GameInterface.Loot);
        lootInterface.GetComponent<LootInterface>().Initialize(items);
        OpenInventoryUnder(lootInterface.transform.GetChild(0));
    }

    public bool IsInterfaceOpen(GameInterface type)
    {
        return interfaces[type].activeSelf;
    }
}

