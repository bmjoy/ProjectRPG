using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private RaycastHit2D raycastHit;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        HandleWorldObjects();
    }

    private void HandleWorldObjects()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject()) //TODO: Should be changed to a global class that holds interface information on wether or not a interface is currently open
        {
            if (raycastHit)
            {
                var worldObject = raycastHit.transform.GetComponent<IWorldObject>();
                Tooltip.Instance.Show(worldObject.TooltipText);
                //Check if we hit anything in the world to interact with
                if (worldObject != null && Input.GetMouseButtonDown(0))
                {
                    playerMovement.SetTarget(worldObject);

                    //Check what we hit and determine the next action
                    switch (worldObject)
                    {
                        case OverworldEnemy enemy:
                            playerMovement.SetTarget(enemy);
                            break;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                playerMovement.SetTarget(null);
                playerMovement.SetDestination(new Vector2(mousepos.x, mousepos.y));
            }
            else
                Tooltip.Instance.Hide();
        }
    }

}
