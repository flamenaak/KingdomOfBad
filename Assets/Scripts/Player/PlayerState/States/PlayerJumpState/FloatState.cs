using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatState : PlayerAirState
{
    public FloatState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (player.Core.CollisionSenses.IsGrounded())
        {
            stateMachine.ChangeState(player.IdleState);
        }
        CheckHang();

       // player.RigidBody.velocity += new Vector2(player.Controller.ReadInputX() * player.WalkSpeed,0);
        CheckAirInput();
        if (player.RigidBody.velocity.y < -0.2f)
        {
            stateMachine.ChangeState(player.FallState);
        }
        else if (player.RigidBody.velocity.y > 0.2f)
        {
            stateMachine.ChangeState(player.RiseState);
        }

    }

    public override void Enter()
    {
        base.Enter();
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
    }
}
