using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanWindUpState : EnemyState
{
    Spearman spearman;
    public SpearmanWindUpState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        spearman = enemy;
        duration = 2f;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        spearman.RigidBody.velocity = Vector2.zero;
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
            stateMachine.ChangeState(spearman.StabState);
            return;
        }

    }

    public override void Update()
    {
        base.Update();
    }
}
