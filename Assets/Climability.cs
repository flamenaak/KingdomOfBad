using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climability : MonoBehaviour
{
    public LayerMask WhatIsInteractable;
    private bool IAmTop;
    public GameObject entity;
    public GameObject Platform;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    float spriteHeight;

    private void setPlatformActive()
    {
        if (IAmTop)
        {
            if (!GameObject.Find("Player").GetComponent<Player>().isCarrying)
            {
                Platform.SetActive(true);
                Platform.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                Platform.SetActive(false);
                Platform.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
        else if(!IAmTop)
        {
            Platform.SetActive(false);
            Platform.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    void Start()
    {

        //Platform.GetComponent<BoxCollider2D>().size = entity.GetComponent<BoxCollider2D>().size;
        spriteHeight = entity.GetComponent<SpriteRenderer>().bounds.min.y;
        IAmTop = false;
        if (entity == null)
        {
            Debug.LogError($"Climability component cannot find parent enity, probably is not assigned.");
        }
        if (Platform == null)
        {
            Debug.LogError($"Climability component cannot find Platform component, probably is not assigned.");
        }
        if(entity.GetComponentInChildren<CollisionSenses>() == null)
        {
            entity.AddComponent<Core>();
            DataCollisionSenses data = entity.AddComponent<DataCollisionSenses>();
            CollisionSenses col = entity.AddComponent<CollisionSenses>();
            col.Data = data;
            data.WhatIsGround = WhatIsGround;
            data.WhatIsInteractable = WhatIsInteractable;
            GroundCheck.position = new Vector3(entity.transform.position.x, spriteHeight - 0.05f, 0);
            col.groundCheck = GroundCheck;
        }
    }

    void Update()
    {
        RaycastHit2D interactableAbove = Physics2D.BoxCast(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.max.y + 0.25f), new Vector2(0.25f, 0.25f), 0, Vector2.up, 0, WhatIsInteractable);
        RaycastHit2D interactableBelow = Physics2D.BoxCast(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.min.y - 0.15f), new Vector2(0.25f, 0.25f), 0, Vector2.down, 0, WhatIsInteractable);
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
            if (interactableBelow && !entity.GetComponentInChildren<CollisionSenses>().IsGrounded())
            {
                IAmTop = true;
            }
            if (!interactableBelow && entity.GetComponentInChildren<CollisionSenses>().IsGrounded())
            {
                IAmTop = false;
            }
        }
        setPlatformActive();
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawCube(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.max.y + 0.25f), new Vector2(0.25f, 0.25f));
        //Gizmos.DrawCube(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.min.y - 0.15f), new Vector2(0.25f, 0.25f));
    }

}
