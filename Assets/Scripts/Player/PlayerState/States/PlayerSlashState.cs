using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashState : PlayerGroundedState
{
    // Start is called before the first frame update
    public PlayerSlashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {

        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (stab)
        {
            stateMachine.ChangeState(player.StabState);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
