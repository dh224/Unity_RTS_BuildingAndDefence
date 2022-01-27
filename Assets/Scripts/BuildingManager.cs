using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public event EventHandler<OnCurrentBuildingTypeChangedEventArgs> OnCurrentBuildingTypeChanged;
    [SerializeField] private Building hqBuilding;
    
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
        hqBuilding.GetComponent<HealthSystem>().OnDied += BuildingManager_HQOnDied;
    }

    private void BuildingManager_HQOnDied(object sender, EventArgs e)
    {
        SoundsManager.Instance.PlaySound(SoundsManager.Sound.GameOver);
        Destroy(hqBuilding); 
        GameOverUI.Instance.Show();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            string errormessage;
            if (currentBuildingType != null)
            {
                if (CanSpawnBuilding(currentBuildingType, UtilsClass.GetMouseWorldPosition(), out errormessage))
                {
                    if (ResourceManager.Instance.CanAfford(currentBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(currentBuildingType.constructionResourceCostArray);
                        //Instantiate(currentBuildingType/**/.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(),currentBuildingType);
                        SoundsManager.Instance.PlaySound(SoundsManager.Sound.BuildingPlaced);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Can not afford! \n" + "need: " + currentBuildingType.GetConstructionResourceCostString(),new TooltipUI.TooltipTimer{timer = 2f}); 
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errormessage,new TooltipUI.TooltipTimer{timer = 2f});
                }
            }
        }
    }

    private bool CanSpawnBuilding(SO_BuildingType buildingType,Vector3 position,out string errorMessage)
    {
        BoxCollider2D boxcollider2D =  buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2Ds =  Physics2D.OverlapBoxAll(position + (Vector3)boxcollider2D.offset, boxcollider2D.size, 0);

        bool isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is NOT clear!";
            return false;
        }

        // min
        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (var collider2D in collider2Ds)
        {
            BuildingTypeHolder aroundBuildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            
            if (aroundBuildingTypeHolder != null)
            {
                if (aroundBuildingTypeHolder.buildingType == buildingType)
                {
                    errorMessage = "Too close to another building with same type";
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
                errorMessage = "";
                return true;
            }
        }
        errorMessage = "Too far from any other buildings. At least 30."; 
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

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
