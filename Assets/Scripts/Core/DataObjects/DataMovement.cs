using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMovement : MonoBehaviour
{
    public LayerMask WhatIsGround;
    
    public LayerMask WhatIsEnemy;

    public float SafetyOffsetX = 0.3f;

    public float DashForce = 0.8f;

    public float AttackRange = 0.5f;

    public float SlashForce = 0.15f;

    public float StabForce = 1f;

    public float WalkSpeed = 10f;
}
