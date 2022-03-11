using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private int acc;
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        stateMachine.ChangeState(player.LiftState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        acc++; 
        if (player.Core.CollisionSenses.IsGrounded())
        {
            Debug.Log("Grounded");
            if (Mathf.Abs(player.RigidBody.velocity.x) == player.RunSpeed)
            {
                stateMachine.ChangeState(player.RunState);
            }
            else if (Mathf.Abs(player.RigidBody.velocity.x) == player.WalkSpeed)
            {
                stateMachine.ChangeState(player.WalkState);
            }
            else if (player.RigidBody.velocity.x == 0)
            {
                Debug.Log("jump to idle");
                stateMachine.ChangeState(player.IdleState);
            }
        }    
    }

    public override void Update()
    {
        base.Update();
    }
}