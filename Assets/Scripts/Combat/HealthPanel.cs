using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HealthPanel : MonoBehaviour
{
    public Slider HealthSlider;
    public TextMeshProUGUI HealthText;
    public Slider ResourceSlider;
    public TextMeshProUGUI ResourceText;

    public Character character;

    public void Init(Character character)
    {
        this.character = character;
        character.Stats.OnHealthChangedCallback += UpdateUI;

        HealthSlider.maxValue = character.Stats.MaxHealth;
        HealthSlider.value = character.Stats.CurrentHealth;
        HealthText.text = $"{character.Stats.CurrentHealth}/{character.Stats.MaxHealth}";
        EnableText(false);

        UpdateUI();
    }

    private void UpdateUI()
    {
        HealthSlider.value = character.Stats.CurrentHealth;
        HealthText.text = ($"{character.Stats.CurrentHealth}/{character.Stats.MaxHealth}");

        //TODO: update resource slider when implemented;
    }

    private void EnableText(bool shouldEnable)
    {
        HealthText.gameObject.SetActive(shouldEnable);
        ResourceText.gameObject.SetActive(shouldEnable);
    }

    public void Expand()
    {
        GetComponent<RectTransform>().localScale *= 1.75f;
        EnableText(true);
    }

    public void Shrink()
    {
        GetComponent<RectTransform>().localScale /= 1.75f;
        EnableText(false);
    }
}
