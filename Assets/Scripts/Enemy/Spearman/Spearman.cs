using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : Enemy
{
    public SpearmanSlashState SlashState { get; set; }
    public SpearmanStabState StabState { get; set; }
    public SpearmanPreSlashState PreSlashState { get; set; }
    public SpearmanAfterStabState AfterStabState { get; set; }
    public SpearmanWindUpState WindUpState { get; set; }
    public SpearmanDeathState SpearmanDeathState { get; set; }

    public GameObject itself;

    public GameObject Platform;

    public bool BossMinion = false;

    public bool IAmTop;

    public SpearmanAI spearmanAI;

    public override void Awake() 
    {
        base.Awake();

        SlashState = new SpearmanSlashState(this, StateMachine, "slash");
        PreSlashState = new SpearmanPreSlashState(this, StateMachine, "preSlash");
        StabState = new SpearmanStabState(this, StateMachine, "stab");
        AfterStabState = new SpearmanAfterStabState(this, StateMachine, "afterStab");
        WindUpState = new SpearmanWindUpState(this, StateMachine, "windUp");
        ChaseState = new EnemyChargeState(this, StateMachine, "move");
        MeleeAttackState = new SpearmanMeleeAttackState(this, StateMachine, "melee");
        Platform.SetActive(false);
        IAmTop = false;
        if (BossMinion)
        {
            DeathState = new SpearmanDeathState(this, StateMachine, "minionDeath");
        }
    }
}
