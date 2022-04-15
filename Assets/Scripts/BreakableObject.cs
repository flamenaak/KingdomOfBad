using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IHasCollider
{
    // Start is called before the first frame update
    public GameObject brokenEntity;

    public LayerMask expectedLayers;

    public void FixedUpdate()
    {
        Collider2D collision = GetComponentInChildren<BoxCollider2D>();
        var colliders = Physics2D.OverlapBoxAll(
            collision.bounds.center,
            collision.bounds.extents,
            0,
            expectedLayers);

        if (colliders.Length > 0)
        {
            Break();
        }
    }

    public BoxCollider2D GetCollider2D()
    {
        return this.gameObject.GetComponent<BoxCollider2D>();
    }

    private void Break()
    {
        
        Destroy(this.gameObject);
        GameObject brokenPiece = Instantiate(brokenEntity, transform.position, Quaternion.identity);
        foreach(Transform child in brokenPiece.transform)
        {
            child.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f) * 3f, Random.Range(0f, 1f) * 3f);
        }
    }
}
