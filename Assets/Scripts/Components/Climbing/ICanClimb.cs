using UnityEngine;
public interface ICanClimb
{
    int ClimbInput { get; }
    void StartClimbing(int climbInput);
    LayerMask LayerMask { get; }
}