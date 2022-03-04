using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    public PlatformEffector2D effector;

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            effector.rotationalOffset = 180f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effector.rotationalOffset = 0f;
        }
    }
}
