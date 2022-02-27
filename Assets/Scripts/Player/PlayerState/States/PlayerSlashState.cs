using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashState : PlayerGroundedState
{
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
        player.DepleteStamina(1);
        player.startSlashCoolDown();
    }

    public override void Exit()
    {

        base.Exit();

    }

    public override void FixedUpdate()
    {
        if (Time.time - startTime >= 0 && Time.time - startTime < 0.36)
        {
            Vector3 slashPosition = player.Core.Movement.DetermineSlashPosition(player.transform);
            player.RigidBody.MovePosition(slashPosition);
        }
        else if (Time.time - startTime > 0.36f)
        {
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
            if (Time.time - startTime >= 1.06f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
    //The raw inputs are required otherwise it doesn't work
    public override void Update()
    {
        base.Update();
        if (Time.time - startTime > 0.36f)
        {
            if (Input.GetButtonDown("Stab") && player.canStab && player.HaveEnoughStamina())
            {
                stateMachine.ChangeState(player.WindUpState);
            }
            else if (Input.GetButtonDown("Slash") && player.canSlash && player.HaveEnoughStamina())
            {
                SwitchSlashState();
            }
            else if (Input.GetButton("Dash") && player.canDashOrEvade && player.HaveEnoughStamina())
            {
                stateMachine.ChangeState(player.EvadeState);
            }
        }
    }

    protected virtual void SwitchSlashState()
    {
        stateMachine.ChangeState(player.SlashState2);
    }
}
