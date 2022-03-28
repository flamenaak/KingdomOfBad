using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    Transform carriable;
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (jump && player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.LiftState);
        } 
        else if (dashAndEvade && player.canDashOrEvade && player.HaveEnoughStamina())
        {
            stateMachine.ChangeState(player.EvadeState);           
        } 
        else if (slash && player.canSlash && player.Core.Combat.Data.currentHealth > 0.0)
        {
            stateMachine.ChangeState(player.SlashState);
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
        else if (xInput != 0)
        {
            stateMachine.ChangeState(player.WalkState);
        }
        else if (player.Core.CollisionSenses.isTouchingClimable() && yInput != 0) 
        {
            stateMachine.ChangeState(player.ClimbUpState);
        }
        else if (!player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.FloatState);
        }
        else
        {
            player.RigidBody.velocity = new Vector2(0, player.RigidBody.velocity.y);
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Stab") && player.canStab && player.HaveEnoughStamina())
        {
            stateMachine.ChangeState(player.WindUpState);
        }
        //Picking up interactable
        else if (Input.GetButtonDown("Interact") && player.Core.CollisionSenses.IsTouchingCarriable() != null && !player.isCarrying)
        {
            player.InteractButton.GetComponent<Animator>().SetBool("pressed", true);
            player.PickUp();
        }
        //Dropping interactable
        else if (Input.GetButtonUp("Interact") && player.isCarrying)
        {
            player.InteractButton.GetComponent<Animator>().SetBool("pressed", false);
            player.Drop();
        }
    }
}