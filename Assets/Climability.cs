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
    public LayerMask WhatIsInteractable;
    public LayerMask WhatIsGround;
    float GroundedRadius = 0.25f;

    void Start()
    {
        IAmTop = false;
        if (Platform == null)
        {
            Debug.LogError($"Climability component cannot find Platform component, probably is not assigned.");
            return;
        }
        var parentCol =GetComponentInParent<IHasCollider>().GetCollider2D();
        BoxCollider2D myNewCollider = gameObject.AddComponent<BoxCollider2D>();
        myNewCollider.bounds.SetMinMax(parentCol.bounds.min, parentCol.bounds.max);
    }

    void Update()
    {
        RaycastHit2D interactableAbove = Physics2D.BoxCast(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.max.y + 0.25f), new Vector2(0.25f, 0.25f), 0, Vector2.up, 0, WhatIsInteractable);
        RaycastHit2D interactableBelow = Physics2D.BoxCast(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.min.y - 0.15f), new Vector2(0.25f, 0.25f), 0, Vector2.down, 0, WhatIsInteractable);
        IAmTop = !interactableAbove && interactableBelow && !IsGrounded();
        Platform.SetActive(IAmTop);
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawCube(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.max.y + 0.25f), new Vector2(0.25f, 0.25f));
        //Gizmos.DrawCube(new Vector2(this.transform.position.x, this.GetComponentInParent<SpriteRenderer>().bounds.min.y - 0.15f), new Vector2(0.25f, 0.25f));
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
