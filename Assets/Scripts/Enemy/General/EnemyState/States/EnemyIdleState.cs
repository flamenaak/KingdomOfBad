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
        if(canSeePlayer && !enemy.aware) {
            enemy.aware = true;
            enemy.Awarness.GetComponent<Animator>().Play("Base Layer.Spotted", 0, 0);
            stateMachine.ChangeState(enemy.MoveState);
            return;
        }
        else if (!canSeePlayer && enemy.aware)
        {
            searchDuration = 3f;
            enemy.Awarness.GetComponent<Animator>().Play("Base Layer.Searching", 0, 0);
            if (Time.time - startTime > searchDuration)
            {
                enemy.aware = false;
                return;
            }
        }
        else if (Time.time - startTime > duration)
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
