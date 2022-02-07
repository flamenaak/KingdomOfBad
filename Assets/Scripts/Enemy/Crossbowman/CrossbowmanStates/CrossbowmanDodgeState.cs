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
        RaycastHit2D wallInTheWay = Physics2D.Raycast(crossbowman.transform.position, Vector2.right * enemy.Core.Movement.GetFacingDirection(), 3f, crossbowman.Core.Movement.Data.WhatIsGround);
        if (wallInTheWay)
        {
            enemy.Core.Movement.Flip();
        }
        crossbowman.RigidBody.velocity = (Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.RunSpeed);
        crossbowman.CanDodge.StartCooldownTimer();
        if (crossbowman.enemyAI.Distance(detectedHostile) >= 7.5f)
        {
            enemy.Core.Movement.Flip();
            if (crossbowman.canShoot)
            {
                stateMachine.ChangeState(crossbowman.RangedAttackState);
            }
            else if(!crossbowman.canShoot)
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
