using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefDeathState : EnemyDeathState
{
    Thief thief;
    public ThiefDeathState(Thief enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        thief = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (thief.Core.CollisionSenses.Data.IAmTop && !thief.Platform.active)
        {
            if (!GameObject.Find("Player").GetComponent<Player>().isCarrying)
                thief.Platform.SetActive(false);
            else
                thief.Platform.SetActive(true);
        }
        else if (!thief.Core.CollisionSenses.Data.IAmTop && thief.Platform.active)
        {
            thief.Platform.SetActive(false);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.RigidBody.velocity = Vector2.zero;
        thief.transform.gameObject.layer = 16;
        thief.tag = "Climable";
        thief.Core.CollisionSenses.isClimable();
    }

    public override void Update()
    {
        base.Update();
    }
}
