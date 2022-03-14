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
        return Distance(entity) > 9 && crossbowman.reloaded;
    }

    public override bool ShouldDodge(Transform entity)
    {
        if (!entity)
            return false;
        return Distance(entity) < 5 && crossbowman.CanDodge && entity.transform.position.y == crossbowman.transform.position.y;
    }

    public override bool ShouldMelleeAttack(Transform entity)
    {
        return false;
    }

    public override bool ShouldRangeAttack(Transform entity)
    {
        if (!entity)
            return false;
        return Distance(entity) >= 5 && crossbowman.reloaded;
    }

}
