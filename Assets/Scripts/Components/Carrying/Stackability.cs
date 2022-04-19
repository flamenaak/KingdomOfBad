using UnityEngine;

public class Stackability : Carriability
{
    public Climability Climable { get; private set;}

    private new void Start()
    {
        base.Start();
        Climable = GetComponentInChildren<Climability>();
    }

    private new void Update()
    {
        base.Update();
    }
}