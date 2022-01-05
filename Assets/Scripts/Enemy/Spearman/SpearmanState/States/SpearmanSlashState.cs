using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanSlashState : EnemyHostileSpottedState
{
    Spearman spearman;
    public SpearmanSlashState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
        enemy.RigidBody.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        DoChecks();
        if (detectedHostile && spearman.spearmanAI.ShouldSlash(detectedHostile))
        {
            enemy.Core.Combat.Attack();
            return;
        }
        else
        {
            base.FixedUpdate();
            return;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
