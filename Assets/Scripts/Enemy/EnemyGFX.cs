using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    // Start is called before the first frame update
    public AIPath path;

    // Update is called once per frame
    void Update()
    {
       if(path.desiredVelocity.x >= 0.01F)
        {
            transform.localScale = new Vector3(10f, 10f, 1f);
        } 
       else if(path.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-10f, 10f, 1f);
        }
    }
}
