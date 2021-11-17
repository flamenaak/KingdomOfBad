using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindUpState : PlayerGroundedState
{
    public int acc = 0;
    public PlayerWindUpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
            acc = 0;
            stateMachine.ChangeState(player.LiftState);
        }
        else if (dashAndEvade && player.canDashOrEvade)
        {
            acc = 0;
            stateMachine.ChangeState(player.EvadeState);
        }
        else if (slash && player.canSlash)
        {
            acc = 0;
            stateMachine.ChangeState(player.SlashState);
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
        else if (xInput != 0)
        {
            acc = 0;
            stateMachine.ChangeState(player.WalkState);
        }
        else if (!player.Controller.m_Grounded)
        {
            acc = 0;
            stateMachine.ChangeState(player.FloatState);
        }
        else
        {
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonUp("Stab") && acc > 21)
        {
            acc = 0;
            stateMachine.ChangeState(player.StabState);
        }
        else if(Input.GetButtonUp("Stab") && acc < 21)
        {
            acc = 0;
            stateMachine.ChangeState(player.IdleState);
        }
    }


}
