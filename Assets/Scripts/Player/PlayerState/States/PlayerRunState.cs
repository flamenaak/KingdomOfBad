using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    ParticleSystem Dust;
    public PlayerRunState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Dust = GameObject.Find("Dust").GetComponent<ParticleSystem>();
        Dust.Play();
    }

    public override void Exit()
    {
        Dust.Stop();
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (jump && player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.RiseState);
        }
        else if (dashAndEvade && player.canDashOrEvade && player.HaveEnoughStamina())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if (slash && player.canSlash && player.HaveEnoughStamina())
        {
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
            stateMachine.ChangeState(player.SlashState);
        }
        else
        {
            player.RigidBody.velocity = new Vector2(player.RunSpeed * xInput, player.RigidBody.velocity.y);
        }

    }

    public override void Update()
    {
        base.Update();
    }
}