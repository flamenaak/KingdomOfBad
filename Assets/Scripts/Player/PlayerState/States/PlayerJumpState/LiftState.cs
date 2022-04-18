using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftState : PlayerAirState
{
    public LiftState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckAirInput();
        if (Time.time - startTime > 0.2f)
        {
            this.stateMachine.ChangeState(player.RiseState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
