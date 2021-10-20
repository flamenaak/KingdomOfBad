using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set;}

    public PlayerIdleState IdleState {get; private set;}
    public PlayerWalkState WalkState {get; private set;}

    public Animator Anim {get; private set;}
    private Rigidbody2D RigidBody;
    public Vector2 Velocity {get; private set;}



    public float WalkSpeed = 5f;
    public float RunSpeed = 10f;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        WalkState = new PlayerWalkState(this, StateMachine, "walk");

        Anim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Velocity = RigidBody.velocity;

        StateMachine.Initialize(IdleState);
    }

    // logic
    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    // physics
    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();

        int input = ReadInputX();
        Debug.Log("Xinput " + input);
        
        if (input != 0)
        {
            RigidBody.velocity = (new Vector2(WalkSpeed * input, 0));
        } else {
            RigidBody.velocity = new Vector2(0, 0);
        }

        Velocity = RigidBody.velocity;
    }

    private int ReadInputX()
    {
        float moveX = Input.GetAxis("Horizontal");
        int normalized = (int)Math.Round(moveX/Math.Abs(moveX));

        return  Math.Abs(moveX) > 0.3 ? normalized : 0;
    }

}