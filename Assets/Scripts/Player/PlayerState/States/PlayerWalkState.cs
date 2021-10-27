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
        
        if (xInput != 0)
        {
            if(acc > 20)
            {      
                    stateMachine.ChangeState(player.RunState);
                    acc = 0;
                
            }
            if (jump)
            {
                stateMachine.ChangeState(player.JumpState);
                acc = 0;
            }
            if (dashAndEvade && player.canDashOrEvade)
            {
                stateMachine.ChangeState(player.DashState);
            }
            else 
            {
                player.Velocity = new Vector2(xInput * player.WalkSpeed, player.Velocity.y);
            } 
        }     
    }

    public override void Update()
    {
        base.Update();
    }
}