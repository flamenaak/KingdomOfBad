using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    public CooldownComponent CanDodge;
    public CooldownComponent CanLunge;

    public bool shouldEvade = false;

    [SerializeField]
    public Vector2 LungeForce = new Vector2(50,100);

    public override void Awake()
    {
        base.Awake();
        DodgeState = new ThiefDodgeState(this, StateMachine, "dodge");
        MeleeAttackState = new ThiefMeleeAttackState(this, StateMachine, "melee");
    }

    public override void Start()
    {
        base.Start();
        if (CanLunge)
            Debug.Log("canLunge ");
        if (CanDodge)
            Debug.Log("canDodge ");
    }
}