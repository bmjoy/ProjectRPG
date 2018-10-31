using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHeroInterface : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonsParent;
    private List<Hero> currentHeroes;
    private List<GameObject> buttons;

    public void DrawButtons(List<Hero> heroes)
    {
        if(buttons ==null)
            buttons = new List<GameObject>();

        ClearButtons();
        currentHeroes = heroes;
        foreach (var hero in currentHeroes)
        {
            var selectedHero = hero;
            var newButton = Instantiate(buttonPrefab, buttonsParent).GetComponent<Button>();
            buttons.Add(newButton.gameObject);

            newButton.onClick.AddListener(() => HireHero(selectedHero));
            newButton.GetComponentInChildren<Text>().text = $"{hero.Name}\n{hero.Class}\nPrice:400";
        }
    }

    private void HireHero(Hero hero)
    {
        Debug.Log($"Hired {hero.Name}");
        if(Party.Instance.IsFull == false && FindObjectOfType<Inventory>().Money >= 400)
        {
            Party.Instance.AddPartyMember(hero);
            currentHeroes.Remove(hero);
            DrawButtons(currentHeroes);
            FindObjectOfType<Inventory>().DecreaseMoney(400);
        }
    }

    private void ClearButtons()
    {
        if (buttons.Count == 0)
            return;
        foreach (var item in buttons)
        {
            Destroy(item);
        }
    }
}
