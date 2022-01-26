using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private SO_BuildingType buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Awake()
    {
        buildingDemolishBtn = transform.Find("PF_BuildingDemolishBtn");
        buildingRepairBtn = transform.Find("PF_buildingRepairBtn");
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
         healthSystem = GetComponent<HealthSystem>();
         buildingType = GetComponent<BuildingTypeHolder>().buildingType;
         healthSystem.SetHealthAmountMax(buildingType.healthAmountMax,true);
         healthSystem.OnDied += HealthSystem_OnDied;
         healthSystem.OnDamage +=HealthSystem_OnDamage;
         healthSystem.OnHealed += HealthSystem_OnHealed;
    }
    
    private void HealthSystem_OnDamage(object sender,EventArgs e){
        ShowBuildingRepairBtn();
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        if (healthSystem.ISFullHealth())
        {
            HideBuildingRepairBtn();
        }
    }

    private void HealthSystem_OnDied(object sender,EventArgs e)
    {
        Destroy(gameObject);
    }
        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            healthSystem.Damage(10);
        }
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }

    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn()
    {
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishBtn()
    {
        if (buildingDemolishBtn != null)
        {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
    
    private void ShowBuildingRepairBtn()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingRepairBtn()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
