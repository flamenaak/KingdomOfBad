using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    public CooldownComponent CanDodge;
    public CooldownComponent CanLunge;

    public bool shouldEvade = false;

    public ThiefLungeState LungeState;

    public override void Awake()
    {
        base.Awake();
        DodgeState = new ThiefDodgeState(this, StateMachine, "dodge");
        MeleeAttackState = new ThiefMeleeAttackState(this, StateMachine, "melee");
        LungeState = new ThiefLungeState(this, StateMachine, "melee");
    }

    public override void Start()
    {
        base.Start();
    }
}