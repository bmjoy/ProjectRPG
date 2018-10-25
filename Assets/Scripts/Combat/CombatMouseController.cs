using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatMouseController : MonoBehaviour
{
    public delegate void OnMouseClickTarget(CharacterVisual combatObject);
    public OnMouseClickTarget OnMouseClickTargetCallback;

    private CombatInterface combatInterface;

    private void Start()
    {
        combatInterface = FindObjectOfType<CombatInterface>();
    }

    private void Update()
    {
        var raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (raycastHit)
        {
            var combatTarget = raycastHit.transform.GetComponent<CharacterVisual>();

            if (combatTarget != null)
            {
                combatInterface.ShowTargetIndicator(true, combatTarget);

                if (Input.GetMouseButtonDown(0))
                {
                    OnMouseClickTargetCallback?.Invoke(combatTarget);
                }
            }
        }
        else
            combatInterface.ShowTargetIndicator(false, null);
    }

}
