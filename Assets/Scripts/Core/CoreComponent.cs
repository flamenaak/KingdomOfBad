using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    private Core core;

    public Core Core {
        get {
            if (core)
                return core;
            Debug.LogError("Missing core");
            return null;
        }

        private set { core = value; }
    }
    
    void Awake()
    {
            Core = transform.parent.GetComponent<Core>();
    }
}
