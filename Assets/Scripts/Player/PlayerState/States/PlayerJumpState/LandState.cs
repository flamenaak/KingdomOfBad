using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : PlayerState
{
    public LandState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Time.time - startTime > 0.4f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
