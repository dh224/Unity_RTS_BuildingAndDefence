using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamage;
    public event EventHandler OnDied;
    public event EventHandler OnHealed; 
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

    public void Heal(int amount)
    {
        healthAmount += amount;
        if (healthAmount > healthAmountMax)
        {
            healthAmount = healthAmountMax;
        }
        OnHealed?.Invoke(this,EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;
        OnHealed?.Invoke(this,EventArgs.Empty);

    }

    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }
}
