using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public float LineOfSight;
    [SerializeField]
    protected Transform playerCheck;

    protected Enemy enemy;

    public virtual void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null)
            Debug.LogError("Enemy AI awake cannot find enemy");
    }

    public bool CanSeePlayer()
    {
        return Physics2D.Raycast(playerCheck.position, enemy.Core.Movement.GetFacingDirection() * Vector2.right, LineOfSight, WhatIsPlayer);        
    }

    public virtual Transform DetectHostile()
    {
        RaycastHit2D hostileHit = Physics2D.Raycast(playerCheck.position, enemy.Core.Movement.GetFacingDirection() * Vector2.right, LineOfSight, WhatIsPlayer);
        if (hostileHit)
        {
            return hostileHit.collider.transform;
        }
        return null;
    }

    public virtual Transform SearchForHostile()
    {
        return DetectHostile();
    }

    public virtual bool ShouldChase(Transform entity)
    {
        return true;
    }

    public virtual bool ShouldDodge(Transform entity)
    {
        return false;
    }

    public virtual bool ShouldMelleeAttack(Transform entity)
    {
        return true;
    }

    public virtual bool ShouldRangeAttack(Transform entity)
    {
        return false;
    }

    public virtual Vector2 DetermineDodgePosition(Vector2 target)
    {
        return Vector2.zero;
    }

    public void OnDrawGizmos()
    {
        if(playerCheck && enemy && enemy.Core && enemy.Core.Movement)
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(enemy.Core.Movement.GetFacingDirection() * Vector2.right * LineOfSight));
    }
}
