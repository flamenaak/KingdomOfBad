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
    }

    public override void Exit()
    {

        base.Exit();
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        /* if (player.transform.localScale.x > 0)
         {
             player.RigidBody.velocity = new Vector2(player.SlashForce, 0);
         }
         else if (player.transform.localScale.x < 0)
         {
             player.RigidBody.velocity = new Vector2(player.SlashForce * -1, 0);
         }
         else if (stab)
         {
             stateMachine.ChangeState(player.StabState);           
         }*/
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
