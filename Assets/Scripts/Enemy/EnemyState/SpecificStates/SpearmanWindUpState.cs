using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanWindUpState : EnemyState
{
    public SpearmanWindUpState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 2f;
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
            stateMachine.ChangeState(enemy.StabState);
            return;
        }

    }

    public override void Update()
    {
        base.Update();
    }
}
