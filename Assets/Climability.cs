using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climability : MonoBehaviour
{
    public LayerMask WhatIsInteractable;
    private bool IAmTop;
    public GameObject entity;
    public GameObject Platform;

    private void setPlatformActive()
    {
        if (IAmTop)
        {
            if (!GameObject.Find("Player").GetComponent<Player>().isCarrying)
            {
                Platform.SetActive(true);
            }
            else
            {
                Platform.SetActive(false);
            }
        }
        else if(!IAmTop)
        {
            Platform.SetActive(false);
        }
    }

    void Start()
    {
        IAmTop = false;
        if (entity == null)
        {
            Debug.LogError($"Climability component cannot find parent enity, probably is not assigned.");
        }
        if (Platform == null)
        {
            Debug.LogError($"Climability component cannot find Platform component, probably is not assigned.");
        }
    }

    void Update()
    {
        RaycastHit2D interactableAbove = Physics2D.BoxCast(new Vector2(this.transform.position.x, this.transform.position.y + 0.25f), new Vector2(0.15f, 0.15f), 0, Vector2.up, 0, WhatIsInteractable);
        RaycastHit2D interactableBelow = Physics2D.BoxCast(new Vector2(this.transform.position.x, this.transform.position.y - 1.1f), new Vector2(0.15f, 0.15f), 0, Vector2.down, 0, WhatIsInteractable);
        if (interactableAbove)
        {
            if (interactableBelow)
            {
                IAmTop = false;
            }
            else if (!interactableBelow)
            {
                IAmTop = false;
            }
        }
        else if (!interactableAbove)
        {
            if (interactableBelow && !entity.GetComponentInChildren<Core>().CollisionSenses.IsGrounded())
            {
                IAmTop = true;
                }
            }
            if (!interactableBelow && entity.GetComponentInChildren<Core>().CollisionSenses.IsGrounded())
            {
                IAmTop = false;
            }
        setPlatformActive();
    }
    
}
