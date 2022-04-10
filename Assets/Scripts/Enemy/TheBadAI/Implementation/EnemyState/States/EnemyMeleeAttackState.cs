using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class EnemyMeleeAttackState : EnemyHostileSpottedState
    {
        public EnemyMeleeAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
            if (detectedHostile == null)
            {
                base.FixedUpdate();
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}