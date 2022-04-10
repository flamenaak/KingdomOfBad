using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadAI
{
    public class BadPathfinder : MonoBehaviour
    {
        private Enemy entity;
        Vector2 currentTarget;
        public Vector2 DesiredDirection;


        // Start is called before the first frame update
        void Start()
        {
            entity = GetComponentInParent<Enemy>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 distance = currentTarget - entity.RigidBody.position;
            DesiredDirection = distance.normalized;
        }
    }
}
