using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftState : PlayerAirState
{
    public LiftState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckAirInput();
        if (Time.time - startTime > 0.2f)
        {
            this.stateMachine.ChangeState(player.RiseState);
        }

    }

    // Update is called once per frame
    void Update()
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

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
