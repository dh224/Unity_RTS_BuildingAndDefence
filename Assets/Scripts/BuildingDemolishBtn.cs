using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private Building building;
    private void Awake()
    {
       transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
       {
           SO_BuildingType buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
           foreach (var resourceCost in buildingType.constructionResourceCostArray)
           {
               ResourceManager.Instance.AddResource(resourceCost.resourceType, Mathf.FloorToInt(resourceCost.amount * 0.5f));
           }
            
            Destroy(building.gameObject);
        });
    }
}
