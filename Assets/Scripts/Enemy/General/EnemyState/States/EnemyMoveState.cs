using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        duration = 3f;
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
        if (detectedHostile)
        {
            enemy.Awarness.GetComponent<Animator>().Play("Base Layer.Spotted", 0, 0);
            stateMachine.ChangeState(enemy.HostileSpottedState);
            return;
        }

        if (enemy.Core.CollisionSenses.IsGrounded())
        {
            var isEdge = enemy.Core.CollisionSenses.IsReachingEdge();
            var isWall = enemy.Core.CollisionSenses.IsTouchingWall();
            var canGoForward = !isEdge && !isWall;

            if (canGoForward)
            {
                enemy.RigidBody.velocity = (Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.WalkSpeed);
            }
            else
            {
                if (isWall) {
                    enemy.Core.Movement.Flip();
                } else {
                    enemy.IdleState.FlipAfterIdle = true;
                    stateMachine.ChangeState(enemy.IdleState);
                    return;
                }                
            }
            if (Time.time - startTime > duration)
            {
                stateMachine.ChangeState(enemy.IdleState);
                return;
            }
        }
    }

    public override void Update()
    {
        base.Update();

    }
}
