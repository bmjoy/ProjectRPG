using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatInterface : MonoBehaviour
{
    public GameObject TurnIndicatorPrefab;
    public GameObject FloatingDamageTextPrefab;

    private GameObject TurnIndicator;
    private Character CurrentFighter;
    private CombatMouseController combatMouseController;

    private void Start()
    {
        CombatController.Instance.OnTurnOrderChanged += OnTurnOrderChanged;
        combatMouseController = FindObjectOfType<CombatMouseController>();
    }

    private void OnTurnOrderChanged(Character character)
    {
        CurrentFighter = character;
        DrawTurnIndicator();
    }

    private void DrawTurnIndicator()
    {
        if(CurrentFighter == null)
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

    public void Button_Attack()
    {
        if (CurrentFighter.IsMyTurn && CurrentFighter.IsHero)
        {
            MessageManager.Instance.ShowMessage("Select a target");
            combatMouseController.OnMouseClickTargetCallback += SelectTarget;
        }
    }

    public void Button_EndTurn()
    {
        if (CurrentFighter.IsMyTurn && CurrentFighter.IsHero)
        {
            CombatController.Instance.NextTurn();
        }
    }

    //TODO: this shouldn't be here
    private void SelectTarget(CharacterVisual target)
    {
        var enemy = target.character;

        switch (enemy)
        {
            case Enemy e:
                enemy.TakeDamage(CurrentFighter.Stats.Damage);
                break;
            case Hero h:
                MessageManager.Instance.ShowMessage("Perhaps you shouldn't target yourself or your team mates");
                return;
        }

        CombatController.Instance.NextTurn();
        combatMouseController.OnMouseClickTargetCallback -= SelectTarget;
    } 
}