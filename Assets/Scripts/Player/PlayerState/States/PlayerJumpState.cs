using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (!player.Jump)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("jump", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("jump", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}