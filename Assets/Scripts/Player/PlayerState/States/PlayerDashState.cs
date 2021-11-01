using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
   
    
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.canDashOrEvade = false;
        player.startDashCoolDown();
    }

    public override void Exit()
    {
        
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if ((player.RigidBody.velocity.x < -0.1f && player.transform.localScale.x > 0)
       || (player.RigidBody.velocity.x > 0.1f && player.transform.localScale.x < 0))
        {
            
        }
        Vector3 dashPosition = new Vector2(player.transform.position.x, player.transform.position.y) + (new Vector2(0.3f, 0) * player.DashForce * player.Controller.ReadInputX());

        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position, player.RigidBody.velocity, player.DashForce, player.layerMask);
        if (raycastHit2D.collider != null)
        {
            dashPosition = raycastHit2D.point;
        }
        player.RigidBody.MovePosition(dashPosition);
        if (Time.time - startTime > 0.52f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
