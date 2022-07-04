using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSittingIdleState : KingState
{

    public KingSittingIdleState(King king, StateMachine stateMachine, string animBoolName) : base(king, stateMachine, animBoolName)
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
        if (king.playerInTheZone)
        {
            stateMachine.ChangeState(king.StandUpState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
