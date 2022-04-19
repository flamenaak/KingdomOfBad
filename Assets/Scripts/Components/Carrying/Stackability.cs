using UnityEngine;

public class Stackability : Carriability
{
    public Transform ClimableOwner;
    public Climability Climable { get; set;}

    private new void Start()
    {
        base.Start();
        if (ClimableOwner == null) {
            Debug.LogError("Climable not assigned");
            return;
        }
        Climable = ClimableOwner.GetComponentInChildren<Climability>();
        if (Climable == null)
            Debug.LogError("Stackability is missing Climable");
    }

    private new void Update()
    {
        base.Update();
    }

    public void PickUp()
    {
        Climable.SetColider(false);
    }

    public void Drop()
    {
        Climable.SetColider(true);
    }
}