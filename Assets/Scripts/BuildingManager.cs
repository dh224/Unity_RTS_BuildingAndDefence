using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public event EventHandler<OnCurrentBuildingTypeChangedEventArgs> OnCurrentBuildingTypeChanged;
    
    public class OnCurrentBuildingTypeChangedEventArgs : EventArgs 
    {
        public SO_BuildingType currentBuildingType;
    }
    public static BuildingManager Instance { get; private set; }
    private SO_BuildingTypeList buildingTypeList;
    private SO_BuildingType currentBuildingType;
    private Camera mainCamera;

    private void Awake()
    {
        buildingTypeList = Resources.Load<SO_BuildingTypeList>(nameof(SO_BuildingTypeList));
        currentBuildingType = null;
        Instance = this;
    }
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (currentBuildingType != null)
            {
                Instantiate(currentBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }
    public void SetCurrentBuildingType(SO_BuildingType buildingType)
    {
        this.currentBuildingType = buildingType;
        OnCurrentBuildingTypeChanged?.Invoke(this,new OnCurrentBuildingTypeChangedEventArgs{currentBuildingType = currentBuildingType});
    }

    public SO_BuildingType GetCurrentBuildingType()
    {
        return currentBuildingType;
    }
}
