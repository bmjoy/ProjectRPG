using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance { get; private set; }

    public GameObject TooltipWindow;
    public Text TooltipText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        TooltipWindow.transform.SetAsLastSibling();
    }

    private void LateUpdate()
    {
        if (TooltipWindow.activeSelf)
            TooltipWindow.transform.position = Input.mousePosition + new Vector3(90, -50,0);
    }

    public void Show(string textToShow)
    {
        TooltipWindow.SetActive(true);
        TooltipText.text = textToShow;
    }

    public void Hide()
    {
        TooltipWindow.SetActive(false);
        TooltipText.text = "";
    }
}
