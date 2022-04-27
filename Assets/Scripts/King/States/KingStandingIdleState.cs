using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingStandingIdleState : KingState
{
    public KingStandingIdleState(King king, StateMachine stateMachine, string animBoolName) : base(king, stateMachine, animBoolName)
    {
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
        if (!king.playerInTheZone)
        {
            stateMachine.ChangeState(king.SitDownState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
