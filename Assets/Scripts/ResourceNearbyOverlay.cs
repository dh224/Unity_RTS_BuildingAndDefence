using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
    [SerializeField] private BuildingGhost buildingGhost;
    private ResourceGeneratorData resourceGeneratorData;
    // Start is called before the first frame update
    void Awake()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearResourceAmount(resourceGeneratorData,buildingGhost.gameObject.transform.position);
        float percentage = Mathf.RoundToInt((float)nearbyResourceAmount * 100f / resourceGeneratorData.maxResourceAmount);
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(percentage + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
