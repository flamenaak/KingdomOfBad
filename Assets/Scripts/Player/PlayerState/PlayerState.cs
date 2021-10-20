using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected float startTime;
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        player.Anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        startTime = -1f;
        player.Anim.SetBool(animBoolName, false);
    }

    // every frame
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