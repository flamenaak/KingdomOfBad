using UnityEngine;

public class CooldownComponent : MonoBehaviour
{
    /// <summary>
    /// Length of cooldown timer
    /// </summary>
    [SerializeField]
    public float CooldownTime;

    /// <summary>
    /// True when the cooldown runs down and resets
    /// </summary>
    private bool IsEnabled = true;

    public CooldownComponent(float cooldownTime) 
    {
        this.CooldownTime = cooldownTime;
    }

    public void Start()
    {
        ResetEnable();
    }

    public void ResetEnable()
    {
        IsEnabled = true;
    }

    /// <summary>
    /// Starts the cooldown timer
    /// </summary>
    public void StartCooldownTimer()
    {
        IsEnabled = false;
        Invoke("ResetEnable", CooldownTime);
    }

    /// <summary>
    /// Overriding casting to boolean for ease of use.
    /// </summary>
    public static implicit operator bool(CooldownComponent value)
    {
        return value.IsEnabled;
    }
} 