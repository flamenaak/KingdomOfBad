using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public float LineOfSight;
    [SerializeField]
    private Transform playerCheck;

    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null)
            Debug.LogError("Enemy AI awake cannot find enemy");
    }

    public bool CanSeePlayer()
    {
        return Physics2D.Raycast(playerCheck.position, enemy.Core.Movement.GetFacingDirection() * Vector2.right, LineOfSight, WhatIsPlayer);
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(enemy.Core.Movement.GetFacingDirection() * Vector2.right * LineOfSight));
    }
}
