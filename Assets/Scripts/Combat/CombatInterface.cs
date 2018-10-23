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

    private void Start()
    {
        CombatController.Instance.OnTurnOrderChanged += OnTurnOrderChanged;
    }

    private void OnTurnOrderChanged(Character character)
    {
        CurrentFighter = character;
        DrawTurnIndicator();
    }

    private void DrawTurnIndicator()
    {
        if (TurnIndicator == null)
            TurnIndicator = Instantiate(TurnIndicatorPrefab, CurrentFighter.Visual.transform);
        else
        {
            TurnIndicator.transform.parent = CurrentFighter?.Visual?.transform;
            TurnIndicator.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
    }

    public void Button_Attack()
    {
        if (CurrentFighter.IsMyTurn && CurrentFighter.IsHero)
        {
            MessageManager.Instance.ShowMessage("Select a target");
            FindObjectOfType<CombatMouseController>().OnMouseClickTargetCallback += SelectTarget;
        }
    }

    public void Button_EndTurn()
    {
        if (CurrentFighter.IsMyTurn && CurrentFighter.IsHero)
        {
            CombatController.Instance.NextTurn();
        }
    }

    //TODO: Take this class under handen.
    //TODO: Better way of showing which enemy is which, health panels not clear enough.
    public void SpawnText(Character characterToSpawnAt, string text)
    {
        var newText = Instantiate(FloatingDamageTextPrefab, RandomizeFloatingTextPosition(characterToSpawnAt), Quaternion.identity, transform);

        newText.GetComponent<TextMeshProUGUI>().text = text;
    }

    private Vector3 RandomizeFloatingTextPosition(Character characterToSpawnAt)
    {
        return Camera.main.WorldToScreenPoint(characterToSpawnAt.Visual.Position + new Vector3(UnityEngine.Random.Range(-0.1f,0.1f), 1.25f, 0));
    }

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
        FindObjectOfType<CombatMouseController>().OnMouseClickTargetCallback -= SelectTarget;
    } 
}