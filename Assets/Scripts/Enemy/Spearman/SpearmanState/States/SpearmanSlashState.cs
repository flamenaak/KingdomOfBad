﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanSlashState : EnemyState
{
    public SpearmanSlashState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.RigidBody.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.Core.Combat.Attack();
        if (!canSeePlayer || enemy.enemyAI.PlayerDistance() > 5)
        {
            stateMachine.ChangeState(enemy.IdleState);
            return;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
