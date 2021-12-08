using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsGround;
    public float LineOfSight;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]

    private Core core;

    void Awake()
    {
        core = GetComponentInParent<Core>();
    }

    public bool CanSeePlayer()
    {
        RaycastHit2D raycast = Physics2D.Raycast(playerCheck.position, core.Movement.GetFacingDirection() * Vector2.right, LineOfSight, (WhatIsPlayer | WhatIsGround));
        if(raycast.collider != null)
        {
            return raycast.collider.tag.Equals("Enemy");         
        }
        return false;
    }
}
