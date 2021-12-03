using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;

    protected float startTime;
    protected string animBoolName;

    public State(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        startTime = -1f;
    }

    // every frame
    /**
    * stuff
    */
    public virtual void Update()
    {
    }

    // every fixed update
    public virtual void FixedUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
