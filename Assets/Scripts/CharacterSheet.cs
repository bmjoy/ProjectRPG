using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour
{
    [Header("Hero Selection")]
    [SerializeField] private GameObject heroSelectionPrefab;
    [SerializeField] private Transform heroSelectionParent;

    [Header("Equipment")]
    [SerializeField] private GameObject equipmentSlotPrefab;
    [SerializeField] private Transform equipmentParent;
    private EquipmentSlot[] equipmentSlots;

    [Header("Stats")]
    [SerializeField] private GameObject textWithTooltipPrefab;
    [SerializeField] private Transform textWithTooltipParent;
    private TextWithTooltip[] statTexts;

    [Header("Inventory")]

    private CrossSceneDataManager dataManager;
    private Hero selectedHero;
    private Inventory inventory;

    private void Start()
    {
        dataManager = FindObjectOfType<CrossSceneDataManager>();
        inventory = FindObjectOfType<Inventory>();
        selectedHero = dataManager.Party[0];
        selectedHero.equipmentManager.OnItemChangedCallback += UpdateStats;
        
        InitHeroSelectionPanel();
        InitializeEquipment();
        InitializeStats();
    }

    private void InitHeroSelectionPanel()
    {
        for (int i = 0; i < dataManager.Party.Count; i++)
        {
            var index = i;
            var hero = dataManager.Party[index];
            var heroSelection = Instantiate(heroSelectionPrefab, heroSelectionParent);
            heroSelection.GetComponentInChildren<Text>().text = $"{hero.Name}\n{hero.CharClass}";
            heroSelection.GetComponent<Button>().onClick.AddListener(() => SelectHero(index));
        }
    }

    private void InitializeEquipment()
    {
        equipmentSlots = new EquipmentSlot[Enum.GetNames(typeof(ItemTypes)).Length];
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            var type = (ItemTypes)i;

            equipmentSlots[i] = Instantiate(equipmentSlotPrefab, equipmentParent).GetComponent<EquipmentSlot>();
            equipmentSlots[i].SetData(selectedHero, type, selectedHero.equipmentManager.GetEquippedItem(type), inventory);
        }
    }

    private void UpdateEquipment()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            var type = (ItemTypes)i;
            equipmentSlots[i].UpdateItem(selectedHero, selectedHero.equipmentManager.GetEquippedItem(type));
        }
    }

    private void InitializeStats()
    {
        statTexts = new TextWithTooltip[selectedHero.Stats.statValues.Count];
        for (int i = 0; i < Enum.GetNames(typeof(StatTypes)).Length; i++)
        {
            var statType = (StatTypes)i;

            statTexts[i] = Instantiate(textWithTooltipPrefab, textWithTooltipParent).GetComponent<TextWithTooltip>();

            if(statType == StatTypes.Health)
                statTexts[i].SetText(statType, $"{selectedHero.Stats.GetCurrentStatValue(statType)}/{selectedHero.Stats.GetBaseStatValue(StatTypes.Health)}");
            else
                statTexts[i].SetText(statType, selectedHero.Stats.GetCurrentStatValue(statType).ToString());

            statTexts[i].SetTooltip(StatStrings.GetStatString(statType));
        }
    }

    private void UpdateStats()
    {
        for (int i = 0; i < Enum.GetNames(typeof(StatTypes)).Length; i++)
        {
            var statType = (StatTypes)i;

            if (statType == StatTypes.Health)
                statTexts[i].SetText(statType, $"{selectedHero.Stats.GetCurrentStatValue(statType)}/{selectedHero.Stats.GetBaseStatValue(StatTypes.Health)}");
            else
                statTexts[i].SetText(statType, selectedHero.Stats.GetCurrentStatValue(statType).ToString());
        }
    }

    private void SelectHero(int index)
    {
        //detach event;
        selectedHero.equipmentManager.OnItemChangedCallback -= UpdateStats;

        //Change hero and attach event
        selectedHero = dataManager.Party[index];
        selectedHero.equipmentManager.OnItemChangedCallback += UpdateStats;
        Debug.Log($"Selected Hero {selectedHero.Name}");

        UpdateEquipment();
        UpdateStats();
        //Change equipment
        //Change stats
        //Show new selected hero as highlighted
    }
}

