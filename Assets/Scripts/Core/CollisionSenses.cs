using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Various collision check spots
    // A position marking where to check if the player is grounded.
    public Transform GroundCheck
    {
        get
        {
            if (groundCheck)
                return groundCheck;
            Debug.LogError("missing ground check on " + Core.transform.parent.name);
            return null;
        }

        private set { groundCheck = value; }
    }

    // A position marking where to check for ceilings
    public Transform CeilingCheck
    {
        get
        {
            if (ceilingCheck)
                return ceilingCheck;
            Debug.LogError("missing ceiling check on " + Core.transform.parent.name);
            return null;
        }

        private set { ceilingCheck = value; }
    }

    // position for checking if there is wall ahead (chest height)
    public Transform WallCheck
    {
        get
        {
            if (wallCheck)
                return wallCheck;
            Debug.LogError("missing wall check on " + Core.transform.parent.name);
            return null;
        }

        private set { wallCheck = value; }
    }

    // position for checking if there is a ledge ahead (tip of the head)
    public Transform LedgeCheck
    {
        get
        {
            if (ledgeCheck)
                return ledgeCheck;
            Debug.LogError("missing ledge check on " + Core.transform.parent.name);
            return null;
        }

        private set { ledgeCheck = value; }
    }

    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    #endregion

    public DataCollisionSenses Data;

    public bool IsFacingRight = true;

    public bool IsTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position,
            Vector2.right * Core.Movement.GetFacingDirection(), Data.WallCheckDistance, Data.WhatIsGround);
    }

    public bool IsTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position,
            Vector2.right * Core.Movement.GetFacingDirection(), Data.WallCheckDistance, Data.WhatIsGround);
    }

    public bool IsReachingEdge()
    {
        RaycastHit2D raycast = Physics2D.Raycast(groundCheck.position + (Vector3)(Vector2.right * Core.Movement.GetFacingDirection() * Data.WallCheckDistance),
            Vector2.down,
            Data.WallCheckDistance,
            Data.WhatIsGround);
        return raycast.collider == null;
    }

    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, Data.GroundedRadius, Data.WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public Transform IsTouchingCarriable()
    {
        Collider2D interactable = Physics2D.OverlapBox(this.transform.position,
         new Vector2(1, 1), 0, Data.WhatIsInteractable);
        if(interactable != null && !interactable.tag.Equals("Climable"))
        {
            return interactable.transform;
        }
        else
        {
            return null;
        }
    }

    public bool isTouchingClimable()
    {
        Collider2D interactable = Physics2D.OverlapBox(this.transform.position,
        new Vector2(1, 1), 0, Data.WhatIsInteractable);
        if (interactable != null && interactable.tag.Equals("Climable"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void isClimable(GameObject itself)
    {
        RaycastHit2D hit = Physics2D.Linecast(itself.transform.position, new Vector2(itself.transform.position.x, itself.transform.position.y + 1f), Data.WhatIsInteractable);
        if (hit)
        {
            itself.tag = "Climable";
        }
        else if (!hit && IsGrounded())
        {
            RaycastHit2D top = Physics2D.Linecast(itself.transform.position, new Vector2(itself.transform.position.x, itself.transform.position.y - 1f), Data.WhatIsInteractable);
            if (top && !IsGrounded())
            {
                Debug.Log(2);
                //Instantiate(Resources.Load("Prefabs/Platform"), new Vector2(itself.transform.position.x, itself.transform.position.y - 0.5f), Quaternion.identity);
            }
            //First in the stack/Bottom
            itself.tag = "NonClimable";
        }
    }

    public Vector2 DetermineLedgePosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * Core.Movement.GetFacingDirection(),
            Data.WallCheckDistance,
            Data.WhatIsGround);
        float xDist = xHit.distance;

        RaycastHit2D yHit = Physics2D.Raycast(
            ledgeCheck.position + (Vector3)(Vector2.right * Data.WallCheckDistance * Core.Movement.GetFacingDirection()),
            Vector2.down,
            ledgeCheck.position.y - wallCheck.position.y,
            Data.WhatIsGround);

        float yDist = yHit.distance;
        return new Vector2(wallCheck.position.x + (Core.Movement.GetFacingDirection() * xDist), ledgeCheck.position.y - yDist);
    }
}
