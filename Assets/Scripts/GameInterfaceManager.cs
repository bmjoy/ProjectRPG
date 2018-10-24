using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameInterfaceManager : MonoBehaviour
{
    public static GameInterfaceManager Instance { get; private set; }

    [SerializeField] private GameObject characterSheet;

    private Dictionary<GameInterface, GameObject> interfaces;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        interfaces = new Dictionary<GameInterface, GameObject>()
        {
            { GameInterface.CharacterSheet, characterSheet }
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.C))
            OpenInterface(GameInterface.CharacterSheet);
    }

    public void OpenInterface(GameInterface interfaceToOpen)
    {
        if (!interfaces.ContainsKey(interfaceToOpen))
        {
            Debug.LogError($"Trying to open {interfaceToOpen} but it doesn't exist");
            return;
        }

        ToggleInterfaceDisableRest(interfaceToOpen);
    }

    public void CloseAllInterfaces()
    {
        foreach (var inter in interfaces)
        {
            inter.Value.SetActive(false);
        }
    }

    private void ToggleInterfaceDisableRest(GameInterface gameInterface)
    {
        foreach (var panel in interfaces)
        {
            if (panel.Key != gameInterface)
                panel.Value.SetActive(false);
            else
                panel.Value.SetActive(!panel.Value.activeSelf);
        }
        Tooltip.Instance.Hide();
    }
}

