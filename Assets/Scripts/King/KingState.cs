using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingState : State
{
    protected King king;
    protected float duration;
    public KingState(King king, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.king = king;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        king.anim.SetBool(animBoolName, true);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void Exit()
    {
        base.Exit();
        king.anim.SetBool(animBoolName, false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
