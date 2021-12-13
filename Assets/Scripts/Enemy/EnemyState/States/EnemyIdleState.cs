using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState {
    public bool FlipAfterIdle {get; set;}

    public EnemyIdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 1.5f;
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
        FlipAfterIdle = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.RigidBody.velocity = Vector2.zero;
        if (canSeePlayer)
        {
            //charge/attack
        }
        else if(Time.time - startTime > duration)
        {
            if (FlipAfterIdle)
            {
                enemy.Core.Movement.Flip();
            }
            stateMachine.ChangeState(enemy.MoveState);
            return;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
