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
        base.FixedUpdate();
        //Check if the wall is in the way, if so flip and dodge other way
        //i'm aware the HasClearPath method should be used, but couldn't determine the target location
        if (crossbowman.Core.Movement.HasClearPath(crossbowman.transform, Vector2.right * crossbowman.Core.Movement.GetFacingDirection() * 15))
        {
            enemy.Core.Movement.Flip();
        }
        crossbowman.RigidBody.velocity = (Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.RunSpeed);
        crossbowman.CanDodge.StartCooldownTimer();
        if (crossbowman.enemyAI.Distance(detectedHostile) >= 7.5f)
        {
            enemy.Core.Movement.Flip();
            if (crossbowman.reloaded)
            {
                stateMachine.ChangeState(crossbowman.RangedAttackState);
            }
            else if(!crossbowman.reloaded)
            {
                stateMachine.ChangeState(crossbowman.CrossbowmanReloadState);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
