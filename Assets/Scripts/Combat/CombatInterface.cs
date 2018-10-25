using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInterface : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform abilityButtonParent;
    [SerializeField] private GameObject TurnIndicatorPrefab;
    [SerializeField] private GameObject TargetIndicatorPrefab;

    private GameObject targetIndicator;
    private GameObject TurnIndicator;
    private Character CurrentFighter;
    private CombatMouseController combatMouseController;
    private Ability selectedAbility;
    private List<GameObject> abilityButtons;

    private void Start()
    {
        combatMouseController = FindObjectOfType<CombatMouseController>();
        CombatController.Instance.OnTurnOrderChanged += OnTurnOrderChanged;
        abilityButtons = new List<GameObject>();
    }

    private void OnTurnOrderChanged(Character character)
    {
        CurrentFighter = character;
        DrawTurnIndicator();

        if (character.IsHero)
            DrawAbilityButtons();
        else
            DestroyAbilityButtons();
    }

    private void DrawTurnIndicator()
    {
        if(CurrentFighter.Visual == null)
        {
            Debug.LogError("We don't have a character to draw our turn indicator at");
            return;
        }

        if (TurnIndicator == null)
            TurnIndicator = Instantiate(TurnIndicatorPrefab, CurrentFighter.Visual.transform);
        else
        {
            TurnIndicator.transform.parent = CurrentFighter.Visual.transform;
            TurnIndicator.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
    }

    public void ShowTargetIndicator(bool shouldShow, CharacterVisual target)
    {
        if (targetIndicator == null)
            targetIndicator = Instantiate(TargetIndicatorPrefab);

        targetIndicator.SetActive(shouldShow);
        if (target)
        {
            targetIndicator.transform.parent = target.transform;
            targetIndicator.transform.position = target.transform.position;
        }
    }

    public void Button_Attack()
    {
        if (CurrentFighter.IsMyTurn && CurrentFighter.IsHero)
        {
            MessageManager.Instance.ShowMessage("Select a target");
            combatMouseController.OnMouseClickTargetCallback += SelectTarget;
        }
    }

    private void DrawAbilityButtons()
    {
        DestroyAbilityButtons();
        foreach (var ability in CurrentFighter.Abilities)
        {
            var button = Instantiate(buttonPrefab, abilityButtonParent);
            abilityButtons.Add(button);

            button.GetComponentInChildren<Text>().text = ability.AbilityName;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                var temp = ability;
                selectedAbility = temp;
                MessageManager.Instance.ShowMessage("Select a target");
                combatMouseController.OnMouseClickTargetCallback += SelectTarget;
            });
        }
    }

    private void DestroyAbilityButtons()
    {
        foreach (var button in abilityButtons)
        {
            Destroy(button);
        }
        abilityButtons = new List<GameObject>();
    }

    public void Button_EndTurn()
    {
        if (CurrentFighter.IsMyTurn && CurrentFighter.IsHero)
        {
            CombatController.Instance.NextTurn();
        }
    }

    private void SelectTarget(CharacterVisual target)
    {
        var enemy = target.character;

        switch (enemy)
        {
            case Enemy e:
                CurrentFighter.Attack(selectedAbility, enemy);
                break;
            case Hero h:
                MessageManager.Instance.ShowMessage("Perhaps you shouldn't target yourself or your team mates");
                return;
        }

        combatMouseController.OnMouseClickTargetCallback -= SelectTarget;
    } 
}