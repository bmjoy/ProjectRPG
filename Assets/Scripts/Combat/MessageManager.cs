using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public TextMeshProUGUI messageText;

    public void ShowMessage(string message, float displayTime = 2)
    {
        StartCoroutine(DisplayMessage(message, displayTime));
    }

    public IEnumerator DisplayMessage(string message, float displayTime = 2)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        yield return new WaitForSeconds(2);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }
}

