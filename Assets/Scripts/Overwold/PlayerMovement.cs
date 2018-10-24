using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private IWorldObject target;
    private Vector3 destination;

    void Start()
    {
        destination = transform.position;
    }

    void Update()
    {
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        if (target != null)
        {
            destination = target.Position;

            if(Vector3.Distance(transform.position, destination) < 0.25f)
                target.Interact();
        }

        if(transform.position != destination)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
            LookTowardsDestination();
        }
    }

    private void LookTowardsDestination()
    {
        var dir = destination - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }


    public void SetTarget(IWorldObject target)
    {
        this.target = target;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
}
