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

    public SpearmanAI spearmanAI;

    public override void Awake() 
    {
        base.Awake();

        SlashState = new SpearmanSlashState(this, StateMachine, "slash");
        PreSlashState = new SpearmanPreSlashState(this, StateMachine, "preSlash");
        StabState = new SpearmanStabState(this, StateMachine, "stab");
        AfterStabState = new SpearmanAfterStabState(this, StateMachine, "afterStab");
        WindUpState = new SpearmanWindUpState(this, StateMachine, "windUp");
        MoveState = new SpearmanMoveState(this, StateMachine, "move");
        IdleState = new SpearmanIdleState(this, StateMachine, "idle");
    }
}
