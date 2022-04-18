using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Platformer : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsPlatform;
    private IHasCollider parent;

    public bool IgnorePlatform;


    private void Start()
    {
        parent = GetComponentInParent<IHasCollider>();
        IgnorePlatform = false;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        Bounds bounds = parent.GetBodyCollider2D().bounds;
        Vector2 overlapSize = bounds.extents * 2;
        overlapSize += new Vector2(0.5f, 0.05f);
        Vector2 newCenter = bounds.center;
        newCenter += Vector2.up;
        var legs = bounds.center - new Vector3(0, bounds.extents.y + 0.2f, 0);

        var platforms = Physics2D.OverlapBoxAll(legs, new Vector2(bounds.size.x * 2, 0.2f), 0, WhatIsPlatform);
        var farCast = Physics2D.OverlapBoxAll(newCenter, overlapSize * 2, 0, WhatIsPlatform);

        // reset all platform in reach
        foreach (Collider2D platform in farCast)
        {
            Physics2D.IgnoreCollision(parent.GetBodyCollider2D(), platform, true);
            Physics2D.IgnoreCollision(parent.GetGroundCheckCollider2D(), platform, true);
        }
        // set collision for the platform under the legs
        foreach (Collider2D platform in platforms)
        {
            Physics2D.IgnoreCollision(parent.GetBodyCollider2D(), platform, IgnorePlatform);
            Physics2D.IgnoreCollision(parent.GetGroundCheckCollider2D(), platform, IgnorePlatform);
        }
    }

    public void OnDrawGizmos()
    {
        if (parent == null) return;

        Bounds bounds = parent.GetBodyCollider2D().bounds;
        Vector2 overlapSize = bounds.extents * 2;
        overlapSize += new Vector2(0.5f, 0.05f);
        Vector2 newCenter = bounds.center;
        newCenter += Vector2.up;
        var legs = bounds.center - new Vector3(0, bounds.extents.y + 0.2f, 0);

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(legs, new Vector2(bounds.size.x * 2, 0.5f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(bounds.center, overlapSize);
    }
}