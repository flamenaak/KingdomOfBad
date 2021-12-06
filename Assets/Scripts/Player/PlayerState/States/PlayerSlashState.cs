using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashState : PlayerGroundedState
{
    public int damagePoint = 1;
    public float pushForce = 2.0F;
    // Start is called before the first frame update
    public PlayerSlashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        Vector3 slashPosition = player.Core.Movement.DetermineSlashPosition(player);
        player.RigidBody.MovePosition(slashPosition);
        player.Attack();
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
