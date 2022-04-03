using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProtoAI : BadAI<Enemy>
{
    

    private float patrolDistance;

    public new void FixedUpdate()
    {
        base.FixedUpdate();
        var targetList = ScanForTargets();
        if (!targetList.Any()) return;

        var chosenTarget = targetList.Max();
        var choosenBehaviour = ChooseBehaviour(chosenTarget);
    }
    
    protected override BadBehaviour<Enemy> ChooseBehaviour(BadTarget target)
    {
        switch(target.Type)
        {
            case BadTargetType.Travel: return new IdleWalkerBehaviourProto(entity,target);
        }

        return new IdleWalkerBehaviourProto(entity,target);
    }

    protected override List<BadTarget> ScanForTargets()
    {
        var targetList = new List<BadTarget>();
        
        // check for hostile
        var hostileTarget = DetectHostile();
        if (hostileTarget)
        {
            targetList.Add(new BadTarget() {
                Type = BadTargetType.Attack,
                Priority = 10,
                GetLocation = () => hostileTarget.position
            });
        }

        // check for enviroment to avoid
        var edge = entity.Core.CollisionSenses.IsReachingEdge();
        if (edge != null)
        {
            targetList.Add(new BadTarget() {
                Type = BadTargetType.Avoid,
                Priority = 20,
                GetLocation = () => (Vector2)edge
            });
        }
        var wall = entity.Core.CollisionSenses.IsTouchingWall();
        if (wall != null)
        {
            targetList.Add(new BadTarget() {
                Type = BadTargetType.Avoid,
                Priority = 20,
                GetLocation = () => (Vector2)wall
            });
        }

        // check for patrol destination
        if (Targets.Find(t => t.Type == BadTargetType.Travel) != null)
        {
            var patrolTarget = Vector2.right * patrolDistance * entity.Core.Movement.GetFacingDirection() + (Vector2)entity.transform.position;
            targetList.Add(new BadTarget() {
                Type = BadTargetType.Travel,
                Priority= 1,
                GetLocation = () => patrolTarget
            });
        }

        return targetList;
    }

    // assume single hostile
    private Transform DetectHostile()
    {
        Collider2D possibleHit = Physics2D.OverlapCircle(playerCheck.position, LineOfSight, WhatIsPlayer);

        if (!possibleHit) return null;

        Vector2 enemyToHostile = possibleHit.transform.position - entity.transform.position;

        float angle = Vector2.Angle(Vector2.right * entity.Core.Movement.GetFacingDirection(), enemyToHostile);
        if (angle > 90) return null;

        RaycastHit2D wallHit = Physics2D.Linecast(entity.transform.position, possibleHit.transform.position, entity.Core.Movement.Data.WhatIsGround);

        if (!wallHit) return possibleHit.transform;

        return null;
    }
}