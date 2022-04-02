using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmanDeathState : EnemyDeathState
{
    Spearman spearman;
    public SpearmanDeathState(Spearman enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        spearman = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (spearman.Core.CollisionSenses.Data.IAmTop && !spearman.Platform.active)
        {
            if(!GameObject.Find("Player").GetComponent<Player>().isCarrying)
                spearman.Platform.SetActive(false);
            else
            spearman.Platform.SetActive(true);
        }
        else if(!spearman.Core.CollisionSenses.Data.IAmTop && spearman.Platform.active)
        {
            spearman.Platform.SetActive(false);
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
        //spearman.transform.gameObject.layer = 16;
        //spearman.tag = "Climable";
        //spearman.Core.CollisionSenses.isClimable();
    }
}
