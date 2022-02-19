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
        crossbowman.startDashGravityEffect();
        crossbowman.CanDodge.StartCooldownTimer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!crossbowman.Core.Movement.HasClearPath(crossbowman.transform, Vector2.right * crossbowman.Core.Movement.GetFacingDirection() * 5))
        {
            enemy.Core.Movement.Flip();
        }
        crossbowman.RigidBody.velocity = (Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.RunSpeed);

        if (crossbowman.enemyAI.ShouldRangeAttack(detectedHostile))
        {
            enemy.Core.Movement.Flip();
            if (crossbowman.reloaded)
            {
                stateMachine.ChangeState(crossbowman.RangedAttackState);
            }
            else if (!crossbowman.reloaded)
            {
                stateMachine.ChangeState(crossbowman.CrossbowmanReloadState);
            }
        }
        else if (Time.time - startTime >= 1.56f)
        {
            stateMachine.ChangeState(crossbowman.HostileSpottedState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
