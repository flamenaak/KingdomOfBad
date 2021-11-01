using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerGroundedState
{
 
    public PlayerEvadeState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.startDashCoolDown();
    }

    public override void Exit()
    {
       base.Exit();
      
    }

    public override void FixedUpdate()
    {
        Vector3 dashPosition = new Vector3(); 
        if(player.transform.localScale.x > 0)
        {
            dashPosition = new Vector2(player.transform.position.x, player.transform.position.y) - (new Vector2(0.2f, 0) * player.DashForce);
        }
        else if(player.transform.localScale.x < 0)
        {
            dashPosition = new Vector2(player.transform.position.x, player.transform.position.y) + (new Vector2(0.2f, 0) * player.DashForce);
        }
            

            RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position, player.RigidBody.velocity, player.DashForce, player.layerMask);
            if (raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;
            }
            player.RigidBody.MovePosition(dashPosition);
        if(Time.time - startTime > 0.3f)
        {
            stateMachine.ChangeState(player.IdleState);
        }        
    }

    public override void Update()
    {
        base.Update();
    }
}
