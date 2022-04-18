using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollisionSenses : MonoBehaviour
{
    public float WallCheckDistance = 0.5f;

    public float GroundedRadius = 0.25f;

    public bool IAmTop = false;

    public LayerMask WhatIsGround;

    public LayerMask WhatIsInteractable;

    public LayerMask WhatIsClimable;

    public LayerMask WhatIsPlatform;

}
