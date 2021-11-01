﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatState : PlayerState
{
    public FloatState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (player.Controller.m_Grounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }


        if (player.RigidBody.velocity.y < -0.2f) {
            stateMachine.ChangeState(player.FallState);
        } else if (player.RigidBody.velocity.y > 0.2f) {
            stateMachine.ChangeState(player.RiseState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}