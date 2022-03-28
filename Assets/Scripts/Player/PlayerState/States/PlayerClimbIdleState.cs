using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerClimbIdleState : PlayerGroundedState
{
    public PlayerClimbIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.RigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (yInput != 0 || xInput != 0)
        {
            player.RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            stateMachine.ChangeState(player.ClimbUpState);
        }
        else if (yInput == 0 && player.Core.CollisionSenses.IsGrounded())
        {
            player.RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            stateMachine.ChangeState(player.IdleState);
        }
        else if (Input.GetButton("Jump") && xInput != 0)
        {
            player.RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            stateMachine.ChangeState(player.LiftState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}