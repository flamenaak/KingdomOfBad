﻿using System.Collections;
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

    public Vector2 PlayersPosition()
    {
        var position = Physics2D.Raycast(playerCheck.position, enemy.Core.Movement.GetFacingDirection() * Vector2.right, LineOfSight, WhatIsPlayer);
        return position.point;
    }

    public void OnDrawGizmos()
    {
        if(playerCheck && enemy && enemy.Core && enemy.Core.Movement)
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(enemy.Core.Movement.GetFacingDirection() * Vector2.right * LineOfSight));
    }

    public float PlayerDistance()
    {
        var distance = Physics2D.Raycast(playerCheck.position, enemy.Core.Movement.GetFacingDirection() * Vector2.right, LineOfSight, WhatIsPlayer);
        return distance.distance;
    }
}
