using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image constructionProgressImage;
    private void Awake()
    {
        constructionProgressImage = transform.Find("Mask").Find("Image").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
