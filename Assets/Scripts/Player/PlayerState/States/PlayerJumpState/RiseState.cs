using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseState : PlayerAirState
{
    public RiseState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        player.RigidBody.velocity = new Vector2(player.RigidBody.velocity.x, 10);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckHang();
        CheckAirInput();
        if (Time.time - startTime > 0.4f)
        {
            if (player.RigidBody.velocity.y < -0.2f)
            {
                stateMachine.ChangeState(player.FallState);
            }
            else if (player.RigidBody.velocity.y < 0.2f)
            {
                stateMachine.ChangeState(player.FloatState);
            }
        }

    }
}
