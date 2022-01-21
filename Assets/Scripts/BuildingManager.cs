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
            if (currentBuildingType != null && CanSpawnBuilding(currentBuildingType,UtilsClass.GetMouseWorldPosition()))
            {
                Instantiate(currentBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

    private bool CanSpawnBuilding(SO_BuildingType buildingType,Vector3 position)
    {
        BoxCollider2D boxcollider2D =  buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2Ds =  Physics2D.OverlapBoxAll(position + (Vector3)boxcollider2D.offset, boxcollider2D.size, 0);

        bool isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear) return false;

        // min
        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            BuildingTypeHolder aroundBuildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            
            if (aroundBuildingTypeHolder != null)
            {
                if (aroundBuildingTypeHolder.buildingType == buildingType)
                {
                    return false;
                }
            }
        }
        //max
        float maxConstructionRadius = 30f;
        collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            BuildingTypeHolder aroundBuildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (aroundBuildingTypeHolder != null)
            {
                return true;
            }
        }
        return false;
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
