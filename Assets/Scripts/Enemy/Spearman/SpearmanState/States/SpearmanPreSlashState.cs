using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanPreSlashState : EnemyState
{
    Spearman spearman;
    public SpearmanPreSlashState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 0.2f;
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
        base.FixedUpdate();
        if (Time.time - startTime > duration)
        {
            stateMachine.ChangeState(spearman.SlashState);
            return;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
