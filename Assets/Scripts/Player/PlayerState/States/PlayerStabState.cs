using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStabState : PlayerGroundedState
{
    public PlayerStabState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.startStabCoolDown();
    }

    public override void Exit()
    {

        base.Exit();
    }

    public override void FixedUpdate()
    {
        Vector3 stabPosition = new Vector3();
        if (player.transform.localScale.x > 0)
        {
            stabPosition = new Vector2(player.transform.position.x, player.transform.position.y) + (new Vector2(0.2f, 0) * player.StabForce);
        }
        else if (player.transform.localScale.x < 0)
        {
            stabPosition = new Vector2(player.transform.position.x, player.transform.position.y) - (new Vector2(0.2f, 0) * player.StabForce);
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position, player.RigidBody.velocity, player.StabForce, player.layerMask);
        if (raycastHit2D.collider != null)
        {
            stabPosition = raycastHit2D.point;
        }
        player.RigidBody.MovePosition(stabPosition);
        if (Time.time - startTime > 0.32f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
