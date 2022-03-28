﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanDeathState : EnemyDeathState
{
    Spearman spearman;
    public SpearmanDeathState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        spearman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.RigidBody.velocity = Vector2.zero;
        spearman.itself.layer = 16;
        spearman.Core.CollisionSenses.isClimable(spearman.itself);
    }
}
