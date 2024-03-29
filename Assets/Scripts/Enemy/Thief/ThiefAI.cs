using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAI : EnemyAI
{
    protected Thief thief;

    Vector2 candidatePoint = new Vector2();
    Vector2 sizeOfGizmo = new Vector2();

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

    /// <summary>
    /// Thief should chase if it sees entity but ai says not to attack it and not to dodge
    /// </summary>
    public override bool ShouldChase(Transform entity)
    {
        if (!entity)
            return false;

        return !(ShouldDodge(entity) || ShouldMelleeAttack(entity));
    }

    public override bool ShouldDodge(Transform entity)
    {
        if (!entity)
            return false;

        if (Mathf.Abs(enemy.transform.position.x - entity.position.x) < 4)
        {
            return thief.shouldEvade || (Random.Range(0f, 1f) > 0.6f && thief.CanDodge);
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
        
        float distance = Vector2.Distance(enemy.transform.position, entity.position);

        if (thief.CanLunge && distance <= thief.Core.Movement.Data.StabDistance)
            return true;
        if (distance <= 1)
            return true;

        return false;
    }

    public override bool ShouldRangeAttack(Transform entity)
    {
        return false;
    }

    public override Vector2 DetermineDodgePosition(Vector2 target)
    {
        Vector2 candidate = thief.shouldEvade ?
        target - (enemy.Core.Movement.GetFacingDirection() * Vector2.right * 5) :
        target + (enemy.Core.Movement.GetFacingDirection() * Vector2.right);

        candidatePoint = candidate;
        sizeOfGizmo = thief.GetComponent<BoxCollider2D>().bounds.size;

        if (isValidDodgeTargetPosition(candidate))
        {
            return candidate;
        }
        else
        {
            candidate = thief.shouldEvade ?
            target + (enemy.Core.Movement.GetFacingDirection() * Vector2.right * 5) :
            target - (enemy.Core.Movement.GetFacingDirection() * Vector2.right);

            return isValidDodgeTargetPosition(candidate) ? candidate : (Vector2) enemy.RigidBody.transform.position;
        }
    }

    private bool isValidDodgeTargetPosition(Vector2 target)
    {
        return (enemy.Core.Movement.CanFit(target, thief.GetComponent<BoxCollider2D>(), new Vector2(0, -0.2f)))
        && (enemy.Core.Movement.HasClearPath(thief.transform, target));
    }
}