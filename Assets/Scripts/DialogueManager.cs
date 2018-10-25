using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject dialoguePanel;

    private List<Button> dialogueButtons;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        dialogueButtons = new List<Button>();
    }

    public void ShowDialogue(Dictionary<string, Action> buttons, string text)
    {
        ClearDialoguePanel();
        foreach (var option in buttons)
        {
            var button = Instantiate(buttonPrefab, dialoguePanel.transform).GetComponent<Button>();
            dialogueButtons.Add(button);
            button.onClick.AddListener(() => { option.Value(); });
            button.GetComponentInChildren<Text>().text = option.Key;
        }
        dialoguePanel.SetActive(true);
    }

    private void ClearDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        foreach (var button in dialogueButtons)
        {
            Destroy(button.gameObject);
        }
    }
}
