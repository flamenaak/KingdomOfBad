using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private int acc;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.DoJump();
        acc = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        acc++; 
        base.FixedUpdate();

        if (player.Controller.Grounded && acc > 20)
        {
            Debug.Log("Grounded");
            if (Mathf.Abs(player.Velocity.x) == player.RunSpeed)
            {
                stateMachine.ChangeState(player.RunState);
            }
            else if (Mathf.Abs(player.Velocity.x) == player.WalkSpeed)
            {
                stateMachine.ChangeState(player.WalkState);
            }
            else if (player.Velocity.x == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }    
    }

    public override void Update()
    {
        base.Update();
    }
}