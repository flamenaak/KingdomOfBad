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
    public LiftState LiftState { get; private set; }
    public RiseState RiseState { get; private set; }
    public FloatState FloatState { get; private set; }
    public FallState FallState { get; private set; }
    public LandState LandState { get; private set; }

    [SerializeField] public LayerMask layerMask;
    public Animator Anim {get; private set;}


    public Rigidbody2D RigidBody;
    public float velocityX;
    public CharacterController2D Controller;
    private CameraMovement camera;

    public bool Crouch = false;
    public bool Jump = false;



    public float WalkSpeed = 5f;
    public float RunSpeed = 10f;
    public float SprintSpeed = 20f;
    public float DashForce = 1f;
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
        LiftState = new LiftState(this, StateMachine, "lift");
        RiseState = new RiseState(this, StateMachine, "rise");
        FloatState = new FloatState(this, StateMachine, "float");
        FallState = new FallState(this, StateMachine, "fall");
        LandState = new LandState(this, StateMachine, "land");

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
    }

    public void OnLanding()
    {
        //StateMachine.ChangeState(IdleState);
    }


    private int ReadInputX()
    {
        float moveX = Input.GetAxis("Horizontal");
        int normalized = (int)Math.Round(moveX/Math.Abs(moveX));

        return  Math.Abs(moveX) > 0.3 ? normalized : 0;
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