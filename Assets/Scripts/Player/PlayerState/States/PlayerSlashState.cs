using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashState : PlayerGroundedState
{
    // Start is called before the first frame update
    public PlayerSlashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.startSlashCoolDown();
    }

    public override void Exit()
    {

        base.Exit();
        
    }

    public override void FixedUpdate()
    {
        
        Vector3 slashPosition = new Vector3();
        if (player.transform.localScale.x > 0)
        {
            slashPosition = new Vector2(player.transform.position.x, player.transform.position.y) + (new Vector2(0.2f, 0) * player.SlashForce);
        }
        else if (player.transform.localScale.x < 0)
        {
            slashPosition = new Vector2(player.transform.position.x, player.transform.position.y) - (new Vector2(0.2f, 0) * player.SlashForce);
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position, player.RigidBody.velocity, player.SlashForce, player.layerMask);
        if (raycastHit2D.collider != null)
        {
            slashPosition = raycastHit2D.point;
        }
        player.RigidBody.MovePosition(slashPosition);
        if (Time.time - startTime > 0.36f)
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void Update()
    {
        base.Update();
    }
}
