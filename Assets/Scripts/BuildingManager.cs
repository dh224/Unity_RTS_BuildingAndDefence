using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private SO_BuildingTypeList buildingTypeList;
    private SO_BuildingType buildingType;
    private Camera mainCamera;

    private void Awake()
    {
        buildingTypeList = Resources.Load<SO_BuildingTypeList>(nameof(SO_BuildingTypeList));
        buildingType = buildingTypeList.list[0]; 
    }
    void Start()
    {
        mainCamera = Camera.main;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            buildingType = buildingTypeList.list[0];
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            buildingType = buildingTypeList.list[1];
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint((Input.mousePosition));
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
