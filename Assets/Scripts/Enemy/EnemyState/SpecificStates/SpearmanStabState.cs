using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanStabState : EnemyState
{
    public SpearmanStabState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 1f;
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
        Vector3 stabPosition = enemy.Core.Movement.DetermineStabPosition(enemy.transform);
        enemy.Attack();
        enemy.RigidBody.MovePosition(stabPosition);
        if (Time.time - startTime > duration)
        {
           stateMachine.ChangeState(enemy.AfterStabState);
           return;
        }
        
    }

    public override void Update()
    {
        base.Update();
    }
}
