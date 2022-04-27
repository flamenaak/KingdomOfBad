using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriability : MonoBehaviour
{
    BoxCollider2D parentCol;
    BoxCollider2D myNewCollider;
    void Start()
    {
        parentCol = GetComponentInParent<IHasCollider>().GetCollider2D();
        myNewCollider = gameObject.AddComponent<BoxCollider2D>();
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
    }

    private void Update()
    {
        myNewCollider.size = new Vector2(parentCol.size.x, parentCol.size.y);
        myNewCollider.offset = new Vector2(parentCol.offset.x, parentCol.offset.y);
    }
}
