using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Describes wheter the target has been fulfilled or not
    /// </summary>
    public bool Completed = false;
    public string Id {get;}

    public BadTarget(string id)
    {
        this.Id = id.ToLower();
    }

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

    public static bool operator == (BadTarget left, BadTarget right)
    {
        return left.Id == right.Id;
    }

    public static bool operator != (BadTarget left, BadTarget right)
    {
        return left.Id != right.Id;
    }

    public static string GetIdFromVector(Vector2 vector2)
    {
        return $"{vector2.x},{vector2.y}";
    }

    public static string GetIdFromGameObject(GameObject obj)
    {
        return obj.GetInstanceID().ToString();
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

public class BadTargetComparer : IEqualityComparer<BadTarget>
{
	public bool Equals(BadTarget? x, BadTarget? y)
	{
		return x?.Id == y?.Id;
	}

	public int GetHashCode(BadTarget obj)
	{
		return obj.Id.GetHashCode();
	}
}