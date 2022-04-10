using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BadTarget
{
    public delegate Vector2 LocationGetter();
    /// <summary>
    /// What should AI do about the target
    /// </summary>
    public BadTargetType Type;
    
    /// <summary>
    /// Gets location of the target
    /// </summary>
    public LocationGetter GetLocation;

    /// <summary>
    /// The higher the priority, the more likely the AI will choose to pursue the target
    /// </summary>
    public int Priority;
    
    public bool Completed = false;

    public static BadTarget operator > (BadTarget left, BadTarget right)
    {
        if (left.Priority == right.Priority)
        {
            return left.Type > right.Type ? left : right;
        }
        
        return left.Priority > right.Priority ? left : right;
    }

    public static BadTarget operator < (BadTarget left, BadTarget right)
    {
        if (left.Priority == right.Priority)
        {
            return left.Type < right.Type ? left : right;
        }
        return left.Priority < right.Priority ? left : right;
    }
}

public enum BadTargetType
{
    Attack = 90,
    Defend = 80,
    Avoid = 70,
    Follow = 60,
    Travel = 50,
}