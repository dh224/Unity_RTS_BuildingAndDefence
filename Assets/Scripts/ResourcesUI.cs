using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private SO_ResourceTypeList resourceTypeList;
    private Dictionary<SO_ResourceType, Transform> resourceTypeTransformDictionary;


    private void Awake()
    {
        resourceTypeTransformDictionary = new Dictionary<SO_ResourceType, Transform>();
        resourceTypeList = Resources.Load<SO_ResourceTypeList>(nameof(SO_ResourceTypeList));
        Transform resourceUITemplate = transform.Find("ResourceTemplate");
        resourceUITemplate.gameObject.SetActive(false);
        int index = 0;
        foreach (var resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceUITemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index++, 0f);
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceTypeTransformDictionary[resourceType] = resourceTransform;
        } 

    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }
    private void UpdateResourceAmount()
    {
        foreach (var resourceType in resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTypeTransformDictionary[resourceType].Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString()); 
        }
    }
}
