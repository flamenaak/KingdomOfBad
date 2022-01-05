using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAI : EnemyAI
{
    protected Thief thief;

    public override void Awake()
    {
        base.Awake();
        thief = GetComponentInParent<Thief>();
        if (enemy == null)
            Debug.LogError("Thief AI awake cannot find thief");
    }

    public override Transform DetectHostile()
    {
        return base.DetectHostile();
    }

    public override bool ShouldChase(Transform entity)
    {
        if (!entity)
            return false;
        return Mathf.Abs(enemy.transform.position.x - entity.position.x) > 3;
    }

    public override bool ShouldDodge(Transform entity)
    {
        if (!entity)
            return false;

        if (Mathf.Abs(enemy.transform.position.x - entity.position.x) < 4 && thief.canDodge)
        {
            return Random.Range(0f, 1f) > 0 && thief.canDodge;
        }
        else
        {
            return false;
        }

    }

    public override bool ShouldMelleeAttack(Transform entity)
    {
        if (!entity)
            return false;

        return !ShouldChase(entity);
    }

    public override bool ShouldRangeAttack(Transform entity)
    {
        return false;
    }

    public override Vector2 DetermineDodgePosition(Vector2 target)
    {   
        if (thief.evadeDodge)
            return DetermineEvadePosition(target);

        Vector2 candidate = target + (enemy.Core.Movement.GetFacingDirection() * Vector2.right);
        if (Physics2D.Linecast(enemy.RigidBody.transform.position, candidate, enemy.Core.CollisionSenses.Data.WhatIsGround))
        {
            candidate = target - (enemy.Core.Movement.GetFacingDirection() * Vector2.right);
        }
        if (Physics2D.Linecast(enemy.RigidBody.transform.position, candidate, enemy.Core.CollisionSenses.Data.WhatIsGround))
        {
            return enemy.RigidBody.transform.position;
        }
        return candidate;
    }

    public Vector2 DetermineEvadePosition(Vector2 target)
    {
        Vector2 candidate = target - (enemy.Core.Movement.GetFacingDirection() * Vector2.right * 5);
        if (Physics2D.Linecast(enemy.RigidBody.transform.position, candidate, enemy.Core.CollisionSenses.Data.WhatIsGround))
        {
            candidate =  target + (enemy.Core.Movement.GetFacingDirection() * Vector2.right * 5);
        }
        if (Physics2D.Linecast(enemy.RigidBody.transform.position, candidate, enemy.Core.CollisionSenses.Data.WhatIsGround))
        {
            Debug.Log("candidates suck");
            return enemy.RigidBody.transform.position; 
        }
        return candidate;
    }
}