using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerGroundedState
{
    float duration;
    public PlayerDamagedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        duration = 0.35f;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void Exit()
    {
        player.Core.Combat.damaged = false;
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time - startTime > duration)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Update()
    {
        base.Update();
    }
}
