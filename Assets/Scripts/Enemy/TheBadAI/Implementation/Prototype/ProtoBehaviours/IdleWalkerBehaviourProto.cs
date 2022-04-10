using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class IdleWalkerBehaviourProto : BadBehaviour<Enemy>
    {
        CooldownComponent hasWaited;    // true if ready to move
        System.Random r = new System.Random();
        List<BadTarget> pathToGoal = new List<BadTarget>();
        EnemyMoveState moveState;
        EnemyIdleState idleState;

        public IdleWalkerBehaviourProto(Enemy entity, BadTarget target, BadPathfinder pathFinder) : base(entity, target, pathFinder)
        {
            moveState = new EnemyMoveState(entity, entity.StateMachine, "move");
            idleState = new EnemyIdleState(entity, entity.StateMachine, "idle");

            hasWaited = entity.gameObject.AddComponent<CooldownComponent>();
        }

        public override void FixedUpdate()
        {
            var core = entity.Core;
            var senses = core.CollisionSenses;
            var movement = core.Movement;

            if (Vector2.Distance(entity.RigidBody.position, Target.GetLocation()) == 0)
            {
                Target.Completed = true;
                goIdle(false);
                return;
            }

            // stay idle
            // if (!hasWaited && !stateMachine.CurrentState.Equals(idleState))
            // {
            //     goIdle(false);
            //     return;
            // }

            // if (hasWaited)
            // {
            //     hasWaited.CooldownTime = (float)(r.Next(1, 3) + r.NextDouble());
            //     hasWaited.StartCooldownTimer();
            // }

            if (pathFinder.DesiredDirection.x < 0 && movement.IsFacingRight
            || pathFinder.DesiredDirection.x > 0 && !movement.IsFacingRight)
                movement.Flip();

            if (senses.IsTouchingWallBool())
            {
                if (senses.IsTouchingLedge())
                {
                    // climb up
                    Debug.Log("I would climb but I cannot yet");
                }
                goIdle(true);
                return;
            }
            else if (senses.IsReachingEdgeBool())
            {
                // if can jump over, jump
                goIdle(true);
                return;
            }
            else
            {
                // if path is clear, no wall, no gap
                entity.StateMachine.ChangeState(moveState);
            }
        }
        public override void Update()
        {
        }

        private void goIdle(bool flip)
        {
            idleState.FlipAfterIdle = flip;
            idleState.SetDuration((float)(1 + r.NextDouble()));
            entity.StateMachine.ChangeState(idleState);
        }
    }
}