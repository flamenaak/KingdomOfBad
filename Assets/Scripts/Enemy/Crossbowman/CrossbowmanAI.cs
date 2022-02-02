using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowmanAI : EnemyAI
{
    protected Crossbowman crossbowman;

    public override void Awake()
    {
        base.Awake();
        crossbowman = GetComponentInParent<Crossbowman>();
        if (enemy == null)
            Debug.LogError("Crossbowman AI awake cannot find crossbowman");
    }

    public override Transform DetectHostile()
    {
        return base.DetectHostile();
    }

    public override Vector2 DetermineDodgePosition(Vector2 target)
    {
        return Vector2.zero;
        
    }

    public override Transform SearchForHostile()
    {
        return base.SearchForHostile();
    }

    public override bool ShouldChase(Transform entity)
    {
        if (!entity)
            return false;
        return Distance(entity) > 9;
    }

    public override bool ShouldDodge(Transform entity)
    {
        if (!entity)
            return false;

        float dist = Distance(entity);
        return (dist < 5f);
    }

    public override bool ShouldMelleeAttack(Transform entity)
    {
        return base.ShouldMelleeAttack(entity);
    }

    public override bool ShouldRangeAttack(Transform entity)
    {
        float dist = Distance(entity);
        return (dist >= 7.5f);
    }

}
