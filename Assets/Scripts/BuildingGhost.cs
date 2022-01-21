using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    private void Awake()
    {
        spriteGameObject = transform.Find("Sprite").gameObject;
        resourceNearbyOverlay = transform.Find("PF_ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        HideSprite();
    }

    private void Start()
    {
        BuildingManager.Instance.OnCurrentBuildingTypeChanged += BuildingManager_OnCurrentBuildingTypeChanged;
    }

    private void BuildingManager_OnCurrentBuildingTypeChanged(object sender,BuildingManager.OnCurrentBuildingTypeChangedEventArgs e)
    {
        SO_BuildingType buildingType =  e.currentBuildingType;
        if (buildingType == null)
        {
            HideSprite();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            ShowSprite(buildingType.buildingSprite);
            resourceNearbyOverlay.Show(e.currentBuildingType.resourceGeneratorData);
        }
    }
    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void ShowSprite(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void HideSprite()
    {
        spriteGameObject.SetActive(false);
    }
}
