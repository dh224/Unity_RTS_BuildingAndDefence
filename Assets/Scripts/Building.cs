using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;

    private SO_BuildingType buildingType;
    // Start is called before the first frame update
    void Start()
    {
         healthSystem = GetComponent<HealthSystem>();
         buildingType = GetComponent<BuildingTypeHolder>().buildingType;
         healthSystem.SetHealthAmountMax(buildingType.healthAmountMax,true);
         healthSystem.OnDied += HealthSystem_OnDied;
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
}
