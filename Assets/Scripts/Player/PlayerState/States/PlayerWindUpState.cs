using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindUpState : PlayerGroundedState
{
    float chargeTime = 0.2f;
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

        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (jump && player.Controller.m_Grounded)
        {
            stateMachine.ChangeState(player.LiftState);
        }
        else if (dashAndEvade && player.canDashOrEvade)
        {
            stateMachine.ChangeState(player.EvadeState);
        }
        else if (slash && player.canSlash)
        {
            stateMachine.ChangeState(player.SlashState);
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
        else if (xInput != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        }
        else if (!player.Controller.m_Grounded)
        {
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
        if (Input.GetButtonUp("Stab"))
        {
            stateMachine.ChangeState(player.StabState);
        }
    }


}
