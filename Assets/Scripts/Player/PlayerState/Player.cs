using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set;}

    public PlayerIdleState IdleState {get; private set;}
    public PlayerWalkState WalkState {get; private set;}
    public PlayerRunState RunState {get; private set;}

    public PlayerSprintState SprintState { get; private set; }
    

    public Animator Anim {get; private set;}
    private Rigidbody2D RigidBody;
    public Vector2 Velocity {get; set;}
    public float velocityX;
    public CharacterController2D Controller;
    private CameraMovement camera;

    public bool Crouch = false;
    public bool Jump = false;



    public float WalkSpeed = 2f;
    public float RunSpeed = 5f;
    public float SprintSpeed = 7f;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        WalkState = new PlayerWalkState(this, StateMachine, "walk");
        RunState = new PlayerRunState(this, StateMachine, "run");
        SprintState = new PlayerSprintState(this, StateMachine, "sprint");

        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Velocity = RigidBody.velocity;
        StateMachine.Initialize(IdleState);
        camera = (CameraMovement)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraMovement");

        if (Controller == null)
            Debug.Log("no controller");
    }

    // logic
    private void Update()
    {
        StateMachine.CurrentState.Update();

        if ((Velocity.x < -0.1f && transform.localScale.x > 0)
        || (Velocity.x > 0.1f && transform.localScale.x < 0))
        {
            Controller.Flip();
        }
    }

    // physics
    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
        RigidBody.velocity = Velocity;

        // int input = ReadInputX();
        // //Debug.Log("Xinput " + input);
        
        // Controller.Move(Crouch, Jump);
        // Jump = false;

        // if (input != 0)
        // {
        //     RigidBody.velocity = (new Vector2(WalkSpeed * input, 0));
                             
  
        //     if (StateMachine.CurrentState == RunState)
        //     {
        //         RigidBody.velocity = (new Vector2(RunSpeed * input, 0));
        //     }
        //     if(StateMachine.CurrentState == SprintState)
        //     {
        //         RigidBody.velocity = (new Vector2(SprintSpeed * input, 0));
        //     }
            
        // } else {
        //     RigidBody.velocity = new Vector2(0, 0);
        // }

        // Velocity = RigidBody.velocity;
        // velocityX = Math.Abs(Velocity.x);
       // Debug.Log(velocityX);       
    }

    public void OnLanding()
    {
        StateMachine.ChangeState(IdleState);
    }

    public void OnCrouching(bool IsCrouching)
    {
        // animator.SetBool("IsCrouching", IsCrouching);
    }

    private int ReadInputX()
    {
        float moveX = Input.GetAxis("Horizontal");
        int normalized = (int)Math.Round(moveX/Math.Abs(moveX));

        return  Math.Abs(moveX) > 0.3 ? normalized : 0;
    }

}