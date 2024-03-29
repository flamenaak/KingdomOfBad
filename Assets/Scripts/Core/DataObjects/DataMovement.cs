﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMovement : MonoBehaviour
{
    public LayerMask WhatIsGround;

    public LayerMask WhatIsEnemy;

    public float SafetyOffsetX = 0.3f;

    public float DashForce = 0.8f;

    public float WalkSpeed = 10f;

    public float SlashForce = 0.15f;

    public float StabForce = 1f;

    public float RunSpeed = 20f;

    public float StabDistance = 4f;
}
