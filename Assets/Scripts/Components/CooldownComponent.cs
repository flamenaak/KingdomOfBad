using UnityEngine;

public class CooldownComponent : MonoBehaviour
{
    /// <summary>
    /// Length of cooldown timer
    /// </summary>
    [SerializeField]
    private float cooldownTime;

    /// <summary>
    /// True when the cooldown runs down and resets
    /// </summary>
    private bool IsEnabled = true;

    public CooldownComponent(float cooldownTime) 
    {
        this.cooldownTime = cooldownTime;
    }

    public void Start()
    {
        resetEnable();
    }

    private void resetEnable()
    {
        IsEnabled = true;
    }

    /// <summary>
    /// Starts the cooldown timer
    /// </summary>
    public void StartCooldownTimer()
    {
        IsEnabled = false;
        Invoke("resetEnable", cooldownTime);
    }

    /// <summary>
    /// Overriding casting to boolean for ease of use.
    /// </summary>
    public static implicit operator bool(CooldownComponent value)
    {
        return value.IsEnabled;
    }
} 