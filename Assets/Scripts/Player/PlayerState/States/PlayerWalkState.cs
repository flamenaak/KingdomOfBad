using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (player.Velocity.x == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("walk", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("walk", false);
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