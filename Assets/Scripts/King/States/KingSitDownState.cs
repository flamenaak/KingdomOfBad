using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSitDownState : KingState
{
    public KingSitDownState(King king, StateMachine stateMachine, string animBoolName) : base(king, stateMachine, animBoolName)
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
        if (Time.time - startTime > duration)
        {
            stateMachine.ChangeState(king.SittingIdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
