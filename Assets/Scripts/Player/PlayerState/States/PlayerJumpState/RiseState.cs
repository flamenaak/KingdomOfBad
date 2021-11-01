using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseState : PlayerState
{
    public RiseState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();

        player.RigidBody.velocity = new Vector2(player.RigidBody.velocity.x, 10);
        player.Controller.m_Grounded = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time - startTime > 0.4f){
            if (player.RigidBody.velocity.y < -0.2f) {
                stateMachine.ChangeState(player.FallState);
            } else if (player.RigidBody.velocity.y < 0.2f) {
                stateMachine.ChangeState(player.FloatState);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
