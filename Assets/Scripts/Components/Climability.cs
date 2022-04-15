using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasCollider {
    BoxCollider2D GetCollider2D();
}

public class Climability : MonoBehaviour
{
    private bool IAmTop;
    public GameObject Platform;
    public Transform GroundCheck;
    public LayerMask WhatIsClimable;
    public LayerMask WhatIsGround;
    float GroundedRadius = 0.25f;
    BoxCollider2D parentCol;
    BoxCollider2D myNewCollider;

    void Start()
    {
        IAmTop = false;
        if (Platform == null)
        {
            Debug.LogError($"Climability component cannot find Platform component, probably is not assigned.");
            return;
        }
        parentCol = GetComponentInParent<IHasCollider>().GetCollider2D();
        myNewCollider = gameObject.AddComponent<BoxCollider2D>();
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
    }

    void Update()
    {
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);

        Platform.GetComponent<BoxCollider2D>().size = parentCol.size;
        Platform.GetComponent<BoxCollider2D>().offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
        Platform.gameObject.transform.localPosition = new Vector3(0,0,0);

        RaycastHit2D climableAbove = Physics2D.BoxCast(new Vector2(myNewCollider.bounds.center.x, myNewCollider.bounds.center.y + myNewCollider.bounds.extents.y + 0.15f),
            new Vector2(myNewCollider.bounds.size.x, 0.2f), 0, Vector2.up, 0, WhatIsClimable);
        RaycastHit2D climableBelow = Physics2D.BoxCast(new Vector2(myNewCollider.bounds.center.x, myNewCollider.bounds.center.y - myNewCollider.bounds.extents.y - 0.15f),
            new Vector2(myNewCollider.bounds.size.x, 0.2f), 0, Vector2.down, 0, WhatIsClimable);

        IAmTop = !climableAbove && climableBelow && !IsGrounded() && !GameObject.FindObjectOfType<Player>().GetComponentInChildren<CariabilityHandler>().isCarrying;
        settingPlatform();
    }

    private void settingPlatform()
    {
        if (IAmTop)
        {
            Platform.SetActive(true);
            Platform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if(!IAmTop)
        {
            Platform.SetActive(false);
            Platform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
}
