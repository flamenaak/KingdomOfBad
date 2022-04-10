using UnityEngine;

namespace BadAI
{
    public class EnemyChargeState : EnemyHostileSpottedState
    {
        public Transform Hostile { get; set; }

        public EnemyChargeState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            enemy.RigidBody.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            enemy.RigidBody.velocity = Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.RunSpeed;
            base.DoChecks();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}