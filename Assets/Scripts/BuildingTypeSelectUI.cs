using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<SO_BuildingType> ignoreBuildingTypeList;
    private Dictionary<SO_BuildingType, Transform> btnTransformDictionary;
    private Transform arrowBtn;
    private void Awake()
    {
       Transform btnTemplate =  transform.Find("BtnTemplate");
       btnTransformDictionary = new Dictionary<SO_BuildingType, Transform>();
       btnTemplate.gameObject.SetActive(false);
       SO_BuildingTypeList buildingTypeList = Resources.Load<SO_BuildingTypeList>(nameof(SO_BuildingTypeList));
       int index = 0;
       arrowBtn = Instantiate(btnTemplate, transform);
       arrowBtn.gameObject.SetActive(true);
       float offset = 120f;
       arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * ++index, 80f);
       arrowBtn.Find("BuildingImage").GetComponent<Image>().sprite = arrowSprite;
       arrowBtn.Find("BuildingImage").GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
       arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
       {
           BuildingManager.Instance.SetCurrentBuildingType(null);
       }); 
       MouseEnterExitEvents mouseEnterExitEvents = arrowBtn.GetComponent<MouseEnterExitEvents>();
       mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
       {
           TooltipUI.Instance.Show("箭头");
       };
       mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
       {
           TooltipUI.Instance.Hide();
       };

       
       foreach (var buildingType in buildingTypeList.list)
       {
           if (!ignoreBuildingTypeList.Contains(buildingType))
           {
               Transform btnTransform = Instantiate(btnTemplate, transform);
               btnTransform.gameObject.SetActive(true);
               offset = 120f;
               btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * ++index, 80f);
               btnTransform.Find("BuildingImage").GetComponent<Image>().sprite = buildingType.buildingSprite;
               btnTransformDictionary[buildingType] = btnTransform;

               btnTransform.GetComponent<Button>().onClick.AddListener(() =>
               {
                   BuildingManager.Instance.SetCurrentBuildingType(buildingType);
               });  
               
               mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
               mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
               {
                   string coststr = buildingType.GetConstructionResourceCostString();
                   if (coststr == "")
                   {
                       TooltipUI.Instance.Show("NO COST"); 
                   }
                   else
                   {
                       TooltipUI.Instance.Show("COST:\n"+coststr);  
                   }
                   
               };
               mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
               {
                   TooltipUI.Instance.Hide();
               };
           }

       }
    }

    private void Start()
    {
        UpdateCurrentBuildingTypeButton();
        BuildingManager.Instance.OnCurrentBuildingTypeChanged += BuildingManager_OnCurrentBuildingTypeChanged;
    }

    private void Update()
    {
        //UpdateCurrentBuildingTypeButton();
    }

    private void BuildingManager_OnCurrentBuildingTypeChanged(object sender, EventArgs e)
    {
        UpdateCurrentBuildingTypeButton();
    }
    private void UpdateCurrentBuildingTypeButton()
    {
        arrowBtn.Find("Selected").gameObject.SetActive(false); 
        foreach (SO_BuildingType buildingType in btnTransformDictionary.Keys)
        {
            btnTransformDictionary[buildingType].Find("Selected").gameObject.SetActive(false);
        }

        SO_BuildingType currentBuildingType = BuildingManager.Instance.GetCurrentBuildingType();
        if (currentBuildingType == null)
        {
            arrowBtn.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            btnTransformDictionary[currentBuildingType].Find("Selected").gameObject.SetActive(true);
        }
    } 
}
