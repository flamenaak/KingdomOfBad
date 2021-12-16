using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    protected Player player;
    protected Input input;


    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool(animBoolName, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool(animBoolName, false);
    }

    // every frame
    /**
    * stuff
    */
    public override void Update()
    {
        base.Update();
        if(player.Controller.GetRespawnInput()){
            player.Respawn();
        }
    }

    // every fixed update
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}