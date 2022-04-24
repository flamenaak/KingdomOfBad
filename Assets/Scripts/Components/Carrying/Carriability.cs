using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriability : MonoBehaviour
{
    protected BoxCollider2D parentCol;
    protected BoxCollider2D myNewCollider;
    protected void Start()
    {
        parentCol = GetComponentInParent<IHasCollider>().GetBodyCollider2D();
        myNewCollider = gameObject.AddComponent<BoxCollider2D>();
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
    }

    protected void Update()
    {
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
    }
}
