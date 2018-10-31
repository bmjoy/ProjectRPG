using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextWithTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public string TooltipText; 

    public void SetText(StatTypes stat, string value)
    {
        text.text = $"<align=left>{stat}<line-height=0.001> \n <align=right>{value}<line-height=1em>";
    }

    public void SetTooltip(string text)
    {
        TooltipText = text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(TooltipText != "")
            Tooltip.Instance.Show(TooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Hide();
    }
}
