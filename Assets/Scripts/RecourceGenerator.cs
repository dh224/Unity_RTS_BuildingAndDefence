using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecourceGenerator : MonoBehaviour
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

    private void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);
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
}
