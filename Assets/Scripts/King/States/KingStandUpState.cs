using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingStandUpState : KingState
{
    public KingStandUpState(King king, StateMachine stateMachine, string animBoolName) : base(king, stateMachine, animBoolName)
    {
        duration = 0.5f;
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
        if(Time.time - startTime > duration)
        {
            stateMachine.ChangeState(king.StandingIdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
