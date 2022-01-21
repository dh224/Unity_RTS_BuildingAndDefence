using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;
    // Start is called before the first frame update
    void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    public static int GetNearResourceAmount(ResourceGeneratorData resourceGeneratorData,Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearByResouceNodeAmount = 0;
        foreach (var collider in collider2DArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearByResouceNodeAmount++; 
                }
                
            }
        }
        nearByResouceNodeAmount = Mathf.Clamp(nearByResouceNodeAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearByResouceNodeAmount;
    }
    private void Start()
    {

        int nearByResouceNodeAmount = GetNearResourceAmount(resourceGeneratorData, transform.position);
        if (nearByResouceNodeAmount == 0)
        {
            //no resourcenode nearby!
            enabled = false;
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) +
                       resourceGeneratorData.timerMax *
                       (1 - (float)nearByResouceNodeAmount / resourceGeneratorData.maxResourceAmount);
        }
        Debug.Log(nearByResouceNodeAmount);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer  += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType,1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return this.resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return (float)timer / (float)timerMax;
    }

    public float GetAmountGeneratorPerSecond()
    {
        return 1 / timerMax;
    }
}
