using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangState : PlayerState
{
    public Vector2 detectedPos;
    public Vector2 ledgePos;

    public PlayerHangState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.transform.position = detectedPos;
        try{
            ledgePos = player.Controller.DetermineLedgePosition();
        } catch (Exception e)
        {
            stateMachine.ChangeState(player.FallState);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.RigidBody.velocity = Vector2.zero;
        player.transform.position = ledgePos - new Vector2(player.xLedgeOffset * player.Controller.GetFacingDirection(), player.yLedgeOffset);
    }

    public override void Update()
    {
        base.Update();
        if (player.Controller.ReadInputY() < 0) 
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(ledgePos.x, ledgePos.y), 2f);
        Debug.Log("position of ledge " + ledgePos.x + " " + ledgePos.y );
    }

}
