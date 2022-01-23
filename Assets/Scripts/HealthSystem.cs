using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamage;
    public event EventHandler OnDied;
    private int healthAmount;
    [SerializeField] private int healthAmountMax;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int demageAmount)
    {
        healthAmount -= demageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnDamage?.Invoke(this,EventArgs.Empty);
        if (isDead())
        {
            OnDied?.Invoke(this,EventArgs.Empty);
        }
    }
    
    public bool isDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax,bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount)
        {
            this.healthAmount = healthAmountMax;
        }
    }
    public bool ISFullHealth()
    {
        return healthAmount == healthAmountMax;
    }
}
