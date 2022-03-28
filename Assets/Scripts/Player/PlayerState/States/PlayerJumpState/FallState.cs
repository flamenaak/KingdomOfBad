using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerAirState
{
    Vector2 startPosition;
    Vector2 endPosition;
    public FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        startPosition = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckAirInput();
        if (player.Core.CollisionSenses.IsGrounded())
        {
            endPosition = player.transform.position;
            if (startPosition.y - endPosition.y >= player.allowedFallDistance)
            {
                player.Damage(player.fallDamage);
                stateMachine.ChangeState(player.StunState);
                return;
            }
            else if (startPosition.y - endPosition.y >= player.deathFallDistance)
            {
                player.Damage(100);
                return;
            }
            stateMachine.ChangeState(player.LandState);
        }

        CheckHang();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Interact") && player.Core.CollisionSenses.IsTouchingCarriable() != null && !player.isCarrying)
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
        else if (player.Core.CollisionSenses.isTouchingClimable() && player.Controller.ReadInputY() != 0)
        {
            player.RigidBody.velocity = Vector2.zero;
            stateMachine.ChangeState(player.ClimbIdleState);
        }
    }
}
