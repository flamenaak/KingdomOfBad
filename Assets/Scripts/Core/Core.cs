using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private CollisionSenses collisionSenses;
    private Movement movement;

    public CollisionSenses CollisionSenses {
        get {
            if(collisionSenses)
                return collisionSenses;
            Debug.LogError("missing collision senses on " + transform.parent.name);
            return null;
        }

        private set { collisionSenses = value; }
    }

    public Movement Movement {
        get {
            if(movement)
                return movement;
            Debug.LogError("missing movement" + transform.parent.name);
            return null;
        }

        private set { movement = value; }
    }

    void Awake()
    {
        collisionSenses = GetComponentInChildren<CollisionSenses>();
        movement = GetComponentInChildren<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
