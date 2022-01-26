using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private SO_ResourceType goldResourceType;
    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int healthamountMax = healthSystem.GetHealthAmountMax();
            int missingHealthAmount = healthamountMax - healthSystem.GetHealthAmount();
            int repairCost = missingHealthAmount / 2;
            ResourceAmount[] resourceAmounts = new ResourceAmount[]
                { new ResourceAmount { resourceType = goldResourceType, amount = repairCost } };
            if (ResourceManager.Instance.CanAfford(resourceAmounts))
            {
                ResourceManager.Instance.SpendResources(resourceAmounts);
                healthSystem.HealFull();
            }
            else
            {
                TooltipUI.Instance.Show("Can not afford the repair cost!",new  TooltipUI.TooltipTimer{timer = 2f});
            }
        });
    }
}