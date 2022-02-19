using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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

    public virtual Transform DetectHostile()
    {
        Collider2D possibleHit = Physics2D.OverlapCircle(playerCheck.position, LineOfSight, WhatIsPlayer);

        if (!possibleHit) return null;

        Vector2 enemyToHostile = possibleHit.transform.position - enemy.transform.position;

        float angle = Vector2.Angle(Vector2.right * enemy.Core.Movement.GetFacingDirection(), enemyToHostile);
        if (angle > 90) return null;

        RaycastHit2D wallHit = Physics2D.Linecast(enemy.transform.position, possibleHit.transform.position, enemy.Core.Movement.Data.WhatIsGround);

        if (!wallHit) return possibleHit.transform;

        return null;
    }

    public virtual Transform SearchForHostile()
    {
        return DetectHostile();
    }

    public virtual bool ShouldChase(Transform entity)
    {
        return !!entity;
    }

    public virtual bool ShouldDodge(Transform entity)
    {
        return false;
    }

    public virtual bool ShouldMelleeAttack(Transform entity)
    {
        if (entity != null)
            return Distance(entity) <= 1;

        return false;
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
        if (playerCheck && enemy && enemy.Core && enemy.Core.Movement)
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(enemy.Core.Movement.GetFacingDirection() * Vector2.right * LineOfSight));
    }

    public float Distance(Transform entity)
    {
        if (!entity)
            return -1;
        return Mathf.Abs(enemy.transform.position.x - entity.position.x);
    }
}

public delegate bool AIDecisionFunction(Transform detectedHostile);

public class DecisionFunction_State_Tuple : System.Tuple<AIDecisionFunction, State>
{
    public AIDecisionFunction DecisionFunction
    {
        get => Item1;
    }

    public State NextState
    {
        get => Item2;
    }

    public DecisionFunction_State_Tuple(AIDecisionFunction item1, State item2) : base(item1, item2)
    {
    }
}
