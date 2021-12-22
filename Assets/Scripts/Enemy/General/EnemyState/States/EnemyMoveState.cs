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
            if (canSeePlayer && !enemy.aware)
            {
                enemy.aware = true;
                enemy.Awarness.GetComponent<Animator>().Play("Base Layer.Spotted", 0, 0);
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
