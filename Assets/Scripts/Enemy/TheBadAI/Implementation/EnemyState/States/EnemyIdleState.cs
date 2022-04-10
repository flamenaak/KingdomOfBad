using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class EnemyIdleState : EnemyState
    {
        public bool FlipAfterIdle { get; set; }

        private const float DURATION = 1.5f;

        public EnemyIdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {
            duration = DURATION;
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
            if (Time.time - startTime > duration)
            {
                if (FlipAfterIdle)
                {
                    enemy.Core.Movement.Flip();
                }
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public void SetDuration(float duration)
        {
            if (duration == 0)
                duration = DURATION;
            
            this.duration = duration;
        }
    }
}