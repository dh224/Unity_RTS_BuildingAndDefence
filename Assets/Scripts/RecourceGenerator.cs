using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecourceGenerator : MonoBehaviour
{
    private SO_BuildingType buildingType;
    private float timer;
    private float timerMax;
    // Start is called before the first frame update
    void Awake()
    {
        buildingType = this.GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.recourceGeneratorData.timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer  += timerMax;
            Debug.Log("generate the resource:" + buildingType.recourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(buildingType.recourceGeneratorData.resourceType,1);
        }
    }
}
