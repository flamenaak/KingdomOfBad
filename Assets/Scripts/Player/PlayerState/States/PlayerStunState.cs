using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.LandDust.Play();
    }

    public override void Exit()
    {
        player.LandDust.Stop();
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time - startTime > player.stunLength)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
