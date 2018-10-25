using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Sprite sprite;
    private DamageData dmgData;
    private CharacterVisual target;

    public void Initialize(Sprite sprite, DamageData dmg, CharacterVisual target)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        dmgData = dmg;
        this.target = target;

        LookTowardsDestination(target.Position);
    }

    private void Update()
    {
        if (target == null)
            Destroy(gameObject);

        transform.position = Vector2.MoveTowards(transform.position, target.Position, 10 * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.Position) < 0.1f)
        {
            //TODO: Callback to combatcontroller that turn is over?
            target.character.Stats.TakeDamage(dmgData);
            CombatController.Instance.NextTurn();
            Destroy(gameObject);
        }
    }

    private void LookTowardsDestination(Vector3 destination)
    {
        var dir = destination - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
