using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatMouseController : MonoBehaviour
{
    public GameObject TargetIndicatorPrefab;
    private GameObject targetIndicator;

    public delegate void OnMouseClickTarget(CharacterVisual combatObject);
    public OnMouseClickTarget OnMouseClickTargetCallback;

    private void Update()
    {
        var raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (raycastHit)
        {
            var combatTarget = raycastHit.transform.GetComponent<CharacterVisual>();

            if (combatTarget != null)
            {
                ShowTargetIndicator(true, combatTarget);

                if (Input.GetMouseButtonDown(0))
                {
                    OnMouseClickTargetCallback?.Invoke(combatTarget);
                }
            }
        }
        else
            ShowTargetIndicator(false, null);
    }

    private void ShowTargetIndicator(bool shouldShow, CharacterVisual target)
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
}
