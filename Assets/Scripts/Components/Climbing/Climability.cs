using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasCollider
{
    BoxCollider2D GetBodyCollider2D();
    Collider2D GetGroundCheckCollider2D();
}

public class Climability : MonoBehaviour
{
    private bool IAmTop;
    public GameObject Platform;
    public Transform GroundCheck;
    public LayerMask WhatIsClimable;
    public LayerMask WhatIsGround;

    [SerializeField]
    private LayerMask singlePlatformLayer;

    float GroundedRadius = 0.25f;

    BoxCollider2D parentCol;
    BoxCollider2D myNewCollider;

    public bool ForcePlatformOff = false;

    void Start()
    {
        IAmTop = false;
        if (Platform == null)
        {
            Debug.LogError($"Climability component cannot find Platform component, probably is not assigned.");
            return;
        }
        parentCol = GetComponentInParent<BoxCollider2D>();
        myNewCollider = gameObject.AddComponent<BoxCollider2D>();
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
    }

    void Update()
    {
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);

        Platform.GetComponent<BoxCollider2D>().size = parentCol.size;
        Platform.GetComponent<BoxCollider2D>().offset = parentCol.offset;

        RaycastHit2D climableAbove = Physics2D.BoxCast(new Vector2(myNewCollider.bounds.center.x, myNewCollider.bounds.center.y + myNewCollider.bounds.extents.y + 0.15f),
            new Vector2(myNewCollider.bounds.size.x, 0.2f), 0, Vector2.up, 0, WhatIsClimable);
        RaycastHit2D climableBelow = Physics2D.BoxCast(new Vector2(myNewCollider.bounds.center.x, myNewCollider.bounds.center.y - myNewCollider.bounds.extents.y - 0.15f),
            new Vector2(myNewCollider.bounds.size.x, 0.2f), 0, Vector2.down, 0, WhatIsClimable);

        IAmTop = !climableAbove && climableBelow && !IsGrounded();
        if (IAmTop)
        {
            Platform.SetActive(true);
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Platform.SetActive(false);
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private int getLayerNumber(LayerMask mask)
    {
        int layerNumber = 0;
        int layer = mask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber;
    }

    public void SetColider(bool active)
    {
        myNewCollider.enabled = active;
    }
}
