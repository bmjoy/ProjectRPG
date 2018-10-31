using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IWorldObject
{
    public string cityName = "Some city";
    public Vector3 Position => transform.position;
    public string TooltipText => $"{cityName}";
    public ShopInventory ShopInventory { get; private set; }
    private List<Hero> availableHeroes;

    public void Interact()
    {
        GameInterfaceManager.Instance.OpenDialogue(castleOptions);
    }

    private Dictionary<string, DialogueOption> castleOptions;

    private void Start()
    {
        ShopInventory = new ShopInventory(UnityEngine.Random.Range(5, 20));
        GetNewHeroList();
        castleOptions = new Dictionary<string, DialogueOption>()
        {
            {"Rest\nPrice: 200", new DialogueOption(false, () => { Rest(); }) },
            {"Sell", new DialogueOption(true, () => { GameInterfaceManager.Instance.OpenShopPanel(ShopInventory); }) },
            {"New Hero", new DialogueOption(true, () => { GameInterfaceManager.Instance.OpenHireHeroInterface(availableHeroes); }) },
            {"Exit", new DialogueOption(false, () => { GameInterfaceManager.Instance.CloseAllInterfaces(); }) }
        };
    }

    private void GetNewHeroList()
    {
        availableHeroes = new List<Hero>();
        for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
        {
            availableHeroes.Add(CharacterRandomizer.GetRandomHero());
        }
    }

    private void Rest()
    {
        if(FindObjectOfType<Inventory>().Money >= 200)
        {
            Party.Instance.HealParty(25);
            FindObjectOfType<Inventory>().DecreaseMoney(200);
        }
    }
}
