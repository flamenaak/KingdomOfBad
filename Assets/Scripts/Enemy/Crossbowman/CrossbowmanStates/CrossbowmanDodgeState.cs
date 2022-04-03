using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowmanDodgeState : EnemyDodgeState
{
    Crossbowman crossbowman;
    public CrossbowmanDodgeState(Crossbowman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        crossbowman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Core.Movement.Flip();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        //DoChecks();
        if(Time.time - startTime < 0.45f)
        {
            Vector2 dashPos = crossbowman.Core.Movement.DetermineDashDestination(crossbowman.transform);
            crossbowman.CanDodge.StartCooldownTimer();
            if (!crossbowman.Core.Movement.HasClearPath(crossbowman.transform, (Vector2)crossbowman.transform.position + Vector2.right * crossbowman.Core.Movement.GetFacingDirection() * 1f) 
                || crossbowman.Core.CollisionSenses.IsReachingEdgeBool())
            {
                enemy.Core.Movement.Flip();
                dashPos = crossbowman.Core.Movement.DetermineDashDestination(crossbowman.transform);
            }
            crossbowman.RigidBody.MovePosition(dashPos);
            return;
        }
        stateMachine.ChangeState(crossbowman.HostileSpottedState);     
    }

    public override void Update()
    {
        base.Update();
    }
}
