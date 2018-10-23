using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private GameObject healthPanelPrefab;

    public Character character;
    public Vector3 Position => transform.position;
    private HealthPanel healthPanel;

    public void Initialize(Character character)
    {
        character.Visual = this;
        GetComponent<SpriteRenderer>().sprite = character.Sprite;
        this.character = character;
        healthPanel = Instantiate(healthPanelPrefab, Camera.main.WorldToScreenPoint(Position + new Vector3(0,.65f,0)), Quaternion.identity, transform.parent).GetComponent<HealthPanel>();
        healthPanel.Init(character);
    }

    public virtual void Update()
    {
        character.Update();
    }

    public void Destroy()
    {
        Destroy(healthPanel.gameObject);
        Destroy(gameObject);
    }

    public void OnMouseEnter()
    {
        healthPanel.Expand();
    }

    public void OnMouseExit()
    {        
        healthPanel.Shrink();
    }
}
