using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

//TODO: hacky hacky this should be combined with the messageManager, both are showing messages on screen.
public class CombatText : MonoBehaviour
{
    [SerializeField] private static GameObject combatTextPrefab;
    [SerializeField] private GameObject CombatTextPrefab;

    private static Transform staticTransform;
    private Transform trans;

    private void Awake()
    {
        combatTextPrefab = CombatTextPrefab;
        trans = transform;
        staticTransform = trans;
    }

    public static void DisplayDamageText(DamageData dmgData, Character target)
    {
        var text = Instantiate(combatTextPrefab, RandomizeFloatingTextPosition(target), Quaternion.identity, staticTransform).GetComponent<TextMeshProUGUI>();

        text.fontSize = dmgData.IsCrit ? 45 : 30;
        text.text = dmgData.IsCrit ? "Crit! \n" + dmgData.Damage : dmgData.Damage.ToString();
    }

    private static Vector3 RandomizeFloatingTextPosition(Character characterToSpawnAt)
    {
        return Camera.main.WorldToScreenPoint(characterToSpawnAt.Visual.Position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 1.5f, 0));
    }
}
