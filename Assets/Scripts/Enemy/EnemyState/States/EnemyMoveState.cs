using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public float duration = 3f;
    public EnemyMoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
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
            if (Time.time - startTime > duration)
            {
                stateMachine.ChangeState(enemy.IdleState);
                return;
            }
        }
        else
        {
            if (enemy.enemyAI.PlayerDistance() <= 5f)
            {
                stateMachine.ChangeState(enemy.PreSlashState);
                return;
            }
            else if (enemy.enemyAI.PlayerDistance() >= 10f)
            {
                stateMachine.ChangeState(enemy.WindUpState);
                return;
            }
        }
    }

    public override void Update()
    {
        base.Update();

    }
}
