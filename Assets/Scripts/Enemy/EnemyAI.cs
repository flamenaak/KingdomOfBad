using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public bool FacingRight = true;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsGround;
    public float LineOfSight;
    public float WallCheckDistance;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform ledgeCheck;

    public int GetFacingDirection()
    {
        return this.FacingRight ? 1 : -1;
    }

    public bool CanSeePlayer()
    {
        RaycastHit2D raycast = Physics2D.Raycast(playerCheck.position, GetFacingDirection() * Vector2.right, LineOfSight, (WhatIsPlayer | WhatIsGround));
        if(raycast.collider != null)
        {
            return raycast.collider.tag.Equals("Enemy");         
        }
        return false;
    }

    public bool IsTouchingWall()
    {
        RaycastHit2D raycast = Physics2D.Raycast(playerCheck.position, GetFacingDirection() * Vector2.right, WallCheckDistance,  WhatIsGround);
        return raycast.collider != null;
    }

    public bool IsTouchingLedge()
    {
        RaycastHit2D raycast = Physics2D.Raycast(ledgeCheck.position, GetFacingDirection() * Vector2.right, WallCheckDistance, WhatIsGround);
        return raycast.collider != null;
    }

    public bool IsReachingEdge()
    {
        RaycastHit2D raycast = Physics2D.Raycast(groundCheck.position,Vector2.down, WallCheckDistance, WhatIsGround);
        return raycast.collider == null;
    }
 
}
