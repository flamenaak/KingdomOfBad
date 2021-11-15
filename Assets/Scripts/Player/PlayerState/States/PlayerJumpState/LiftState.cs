using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftState : PlayerAirState
{
    public LiftState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        
    }
}
