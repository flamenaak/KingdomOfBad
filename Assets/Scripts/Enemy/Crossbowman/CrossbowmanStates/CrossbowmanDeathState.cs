using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CrossbowmanDeathState : EnemyDeathState
{
    Crossbowman crossbowman;
    public CrossbowmanDeathState(Crossbowman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        crossbowman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (crossbowman.Core.CollisionSenses.Data.IAmTop && !crossbowman.Platform.active)
        {
            if (!GameObject.Find("Player").GetComponent<Player>().isCarrying)
                crossbowman.Platform.SetActive(false);
            else
                crossbowman.Platform.SetActive(true);
        }
        else if (!crossbowman.Core.CollisionSenses.Data.IAmTop && crossbowman.Platform.active)
        {
            crossbowman.Platform.SetActive(false);
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
        crossbowman.itself.layer = 16;
        crossbowman.Core.CollisionSenses.isClimable(crossbowman.itself);
        crossbowman.tag = "Climable";
        Enter();
    }
}