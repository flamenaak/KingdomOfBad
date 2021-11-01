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
    public PlayerDashState DashState { get; private set; }
    public PlayerEvadeState EvadeState { get; private set; }
    public PlayerJumpState JumpState {get; private set;}


    [SerializeField] public LayerMask layerMask;
    public Animator Anim {get; private set;}
    public Rigidbody2D RigidBody;
    public float velocityX;
    public CharacterController2D Controller;
    private CameraMovement camera;

    public bool Crouch = false;
    public bool Jump = false;



    public float WalkSpeed = 2f;
    public float RunSpeed = 5f;
    public float SprintSpeed = 7f;
    public float DashForce = 10f;
    public float DashCooldown = 3f;
    public bool canDashOrEvade = true;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        WalkState = new PlayerWalkState(this, StateMachine, "walk");
        RunState = new PlayerRunState(this, StateMachine, "run");
        DashState = new PlayerDashState(this, StateMachine, "dash");
        EvadeState = new PlayerEvadeState(this, StateMachine, "evade");
        JumpState = new PlayerJumpState(this, StateMachine, "jump");

        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);

    }

    private void Start()
    {
        camera = (CameraMovement)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraMovement");

        if (Controller == null)
            Debug.Log("no controller");
    }

    // logic
    private void Update()
    {
        StateMachine.CurrentState.Update();
        Vector2 velocity = RigidBody.velocity;

        if ((velocity.x < -0.1f && transform.localScale.x > 0)
        || (velocity.x > 0.1f && transform.localScale.x < 0))
        {
            Controller.Flip();
        }
    }

    // physics
    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
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


    private int ReadInputX()
    {
        float moveX = Input.GetAxis("Horizontal");
        int normalized = (int)Math.Round(moveX/Math.Abs(moveX));

        return  Math.Abs(moveX) > 0.3 ? normalized : 0;
    }

    public void DoJump()
    {
        Controller.Move(false, true);
    }

    public IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(DashCooldown);
    }

    public IEnumerator StartDashOrEvadeCooldown()
    {
        canDashOrEvade = false;
        yield return StartCoroutine("WaitAndPrint");
        canDashOrEvade = true;
    }

    public void startDashCoolDown()
    {
        canDashOrEvade = false;
        Invoke("clearDashOrEvadeCooldown", DashCooldown);
    }

    void clearDashOrEvadeCooldown()
    {
        canDashOrEvade = true;
    }
}