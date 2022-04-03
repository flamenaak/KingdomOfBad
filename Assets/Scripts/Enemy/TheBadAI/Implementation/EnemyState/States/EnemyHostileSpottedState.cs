using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class EnemyHostileSpottedState : EnemyState
    {
        public EnemyHostileSpottedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!detectedHostile)
            {
                stateMachine.ChangeState(enemy.SearchState);
                return;
            }

            List<DecisionFunction_State_Tuple> decisionFunctions = enemy.DecisionFunctions;
            foreach (DecisionFunction_State_Tuple tuple in decisionFunctions)
            {
                if (tuple.DecisionFunction(detectedHostile))
                {
                    stateMachine.ChangeState(tuple.NextState);
                    return;
                }
            }

            stateMachine.ChangeState(enemy.SearchState);
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}