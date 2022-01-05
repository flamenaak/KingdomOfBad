using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Enemy
{
    [SerializeField]
    private float dodgeCooldown = 2f;
    public bool canDodge = true;
    public bool evadeDodge = false;

    [SerializeField]
    public Vector2 LungeForce = new Vector2(50,100);

    public new void Awake()
    {
        base.Awake();
        DodgeState = new ThiefDodgeState(this, StateMachine, "dodge");
        MeleeAttackState = new ThiefMeleeAttackState(this, StateMachine, "melee");
    }

    private void resetDodge()
    {
        canDodge = true;
    }

    public void StartDodgeCooldown()
    {
        canDodge = false;
        Invoke("resetDodge", dodgeCooldown);
    }
}