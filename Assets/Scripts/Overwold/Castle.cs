using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IWorldObject
{
    public string cityName = "Some city";
    public Vector3 Position => transform.position;
    public string TooltipText => $"A place for you party to rest, hire new help and sell items";

    public void Interact()
    {
        DialogueManager.Instance.ShowDialogue(castleOptions, cityName);
    }

    private Dictionary<string, Action> castleOptions;

    private void Start()
    {
        castleOptions = new Dictionary<string, Action>()
        {
            {"Rest", () => { Debug.Log("Rest"); } },
            {"Sell", () => { Debug.Log("Sell"); } },
            {"New Hero", () => { Debug.Log("new Hero"); } }
        };
    }
}
