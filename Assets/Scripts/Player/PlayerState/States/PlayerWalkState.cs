using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public int acc = 0;
    public PlayerWalkState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        acc = 0;
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        acc++;
        
        if (jump && player.Controller.m_Grounded)
        {
            stateMachine.ChangeState(player.JumpState);
        } else if (xInput != 0)
        {
            if (dashAndEvade && player.canDashOrEvade)
            {
                stateMachine.ChangeState(player.DashState);
            }
            else if(acc > 20)
            {      
                    stateMachine.ChangeState(player.RunState);
                    acc = 0;
                
            }
            else 
            {
                player.RigidBody.velocity = new Vector2(xInput * player.WalkSpeed, player.RigidBody.velocity.y);
            } 
        }     
    }

    public override void Update()
    {
        base.Update();
    }
}