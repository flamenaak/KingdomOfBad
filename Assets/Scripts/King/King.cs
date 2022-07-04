using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    public KingSittingIdleState SittingIdleState { get; set; } 
    public KingStandUpState StandUpState { get; set; }
    public KingStandingIdleState StandingIdleState { get; set; }
    public KingSitDownState SitDownState { get; set; }

    public GameObject reactionZone;
    public LayerMask WhatIsPlayer;
    public Animator anim;
    public bool playerInTheZone;

    private void Awake()
    {
        playerInTheZone = false;
        StateMachine = new StateMachine();
        SittingIdleState = new KingSittingIdleState(this, StateMachine, "sitIdle");
        StandUpState = new KingStandUpState(this, StateMachine, "standUp");
        StandingIdleState = new KingStandingIdleState(this, StateMachine, "standIdle");
        SitDownState = new KingSitDownState(this, StateMachine, "sitDown");
    }

    private void Start()
    {
        StateMachine.Initialize(SittingIdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();    
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
        Collider2D collision = reactionZone.GetComponent<BoxCollider2D>();

        var colliders = Physics2D.OverlapCircleAll(collision.bounds.center, collision.bounds.extents.magnitude, WhatIsPlayer);

        if (colliders.Length > 0)
        {
            playerInTheZone = true;
        }
        else
        {
            playerInTheZone = false;
        }
    }
}
