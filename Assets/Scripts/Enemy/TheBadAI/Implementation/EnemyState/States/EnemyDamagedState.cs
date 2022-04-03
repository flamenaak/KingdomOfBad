using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class EnemyDamagedState : EnemyState
    {
        public EnemyDamagedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {
            duration = 0.35f;
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
            enemy.Core.Combat.damaged = false;
            base.Exit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (Time.time - startTime > duration)
            {
                if (!enemy.enemyAI.DetectHostile())
                {
                    enemy.Core.Movement.Flip();
                }
                stateMachine.ChangeState(enemy.IdleState);
                return;
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}