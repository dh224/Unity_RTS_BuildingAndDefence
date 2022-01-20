using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private void Awake()
    {
        spriteGameObject = transform.Find("Sprite").gameObject;
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
        }
        else
        {
            ShowSprite(buildingType.buildingSprite);
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
