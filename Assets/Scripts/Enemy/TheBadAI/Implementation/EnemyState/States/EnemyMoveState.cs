using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
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

        public void test(){}

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Debug.Log("shmoving");
            if (enemy.Core.CollisionSenses.IsGrounded())
            {
                Debug.Log($"Facing direction : {enemy.Core.Movement.GetFacingDirection()}. WalkSpeed: {enemy.Core.Movement.Data.WalkSpeed} total velocity {(Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.WalkSpeed).magnitude}");
                enemy.RigidBody.velocity = (Vector2.right * enemy.Core.Movement.GetFacingDirection() * enemy.Core.Movement.Data.WalkSpeed);
            } else {
                Debug.Log("not grounded but walking");
            }
        }

        public override void Update()
        {
            base.Update();

        }
    }
}