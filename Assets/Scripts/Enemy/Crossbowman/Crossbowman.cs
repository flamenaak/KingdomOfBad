using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbowman : Enemy
{
    public CooldownComponent CanDodge;

    public bool reloaded = true;
    public GameObject bolt;
    public CrossbowmanReloadState CrossbowmanReloadState { get; set; }
    public override List<DecisionFunction_State_Tuple> DecisionFunctions
    {
        get
        {
            return new List<DecisionFunction_State_Tuple> {
               new DecisionFunction_State_Tuple(ShouldReload, CrossbowmanReloadState),
               new DecisionFunction_State_Tuple(enemyAI.ShouldRangeAttack, RangedAttackState),
               new DecisionFunction_State_Tuple(enemyAI.ShouldDodge, DodgeState),
               new DecisionFunction_State_Tuple(enemyAI.ShouldChase, ChaseState)
               };
        }
    }

    private bool ShouldReload(Transform entity)
    {
        if(!entity)
        {
            return false;
        }
        return enemyAI.Distance(entity) >= 5 && !reloaded;
    }

    public override void Awake()
    {
        base.Awake();
        DodgeState = new CrossbowmanDodgeState(this, StateMachine, "dodge");
        RangedAttackState = new CrossbowmanShootState(this, StateMachine, "shoot");
        CrossbowmanReloadState = new CrossbowmanReloadState(this, StateMachine, "reload");
    }

    public void Fire()
    {
        Instantiate(bolt, base.Core.Combat.AttackPosition.position, Quaternion.identity);
        bolt.transform.position = base.Core.Combat.AttackPosition.transform.position;
    }
}
