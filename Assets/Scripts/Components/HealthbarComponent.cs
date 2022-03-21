using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarComponent : MonoBehaviour
{
    private IHasCombat entity;
    private Vector3 originalScale;

    void Start()
    {
        entity = GetComponentInParent<IHasCombat>();
        if (entity == null)
            Debug.LogError($"Healthbar component cannot find game object implementing {nameof(IHasCombat)} in {transform.root}");

        originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        var percentage = entity.Combat.Data.currentHealth / entity.Combat.Data.maxHealth;        
        transform.localScale = new Vector3(originalScale.x * percentage, originalScale.y, originalScale.z);
    }
}
