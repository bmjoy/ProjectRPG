using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour, IWorldObject
{
    public float movementSpeed;
    public List<Enemy> enemies;
    public Vector3 Position => transform.position;

    private Vector3 destination;

    public string TooltipText
    {
        get
        {
            var text = "";
            enemies.ForEach(enemy => text += enemy.Name + "\n");
            return text;
        }
    }

    void Start()
    {
        destination = transform.position;
        movementSpeed = Random.Range(0.2f, 1f);
    }

    void Update()
    {
        if (transform.position != destination)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        }
        else
        {
            GetNewDestination();
        }
    }

    private void GetNewDestination()
    {
        destination = (Vector2)transform.position + Random.insideUnitCircle * 2;
        LookTowardsDestination();
    }

    private void LookTowardsDestination()
    {
        var dir = destination - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void Interact()
    {
        FindObjectOfType<CrossSceneDataManager>().StartCombat(this);
    }
}