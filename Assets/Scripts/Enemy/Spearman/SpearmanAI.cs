using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanAI : EnemyAI
{
    public Spearman spearman;
    public override void Awake()
    {
        base.Awake();
    }

    public override Transform DetectHostile()
    {
        return base.DetectHostile();
    }

    public override Vector2 DetermineDodgePosition(Vector2 target)
    {
        return base.DetermineDodgePosition(target);
    }

    public override Transform SearchForHostile()
    {
        return base.SearchForHostile();
    }

    public override bool ShouldChase(Transform entity)
    {
        if (!entity)
            return false;
        return Distance(entity) > 10;
    }

    public override bool ShouldDodge(Transform entity)
    {
        return false;
    }

    public override bool ShouldMelleeAttack(Transform entity)
    {
        return (!!entity) && (ShouldSlash(entity) || ShouldStab(entity)) && entity.position.y == spearman.transform.position.y;
    }

    public override bool ShouldRangeAttack(Transform entity)
    {
        return false;
    }

    public bool ShouldSlash(Transform entity)
    {
        return Distance(entity) <= 5f;
    }

    public bool ShouldStab(Transform entity)
    {
        float dist = Distance(entity);
        return (dist >= 5f && dist <= 10f);
    }
}
