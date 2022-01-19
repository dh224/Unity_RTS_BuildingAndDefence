using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private SO_BuildingTypeList buildingTypeList;
    private SO_BuildingType buildingType;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;

        buildingTypeList = Resources.Load<SO_BuildingTypeList>(typeof(SO_BuildingTypeList).Name);
        buildingType = buildingTypeList.list[0];
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
