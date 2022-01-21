using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorOverlay : MonoBehaviour
{

    [SerializeField] private ResourceGenerator resourceGenerator;

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

    }

    private void Update()
    {
        transform.Find("Bar").localScale = new Vector3(1-resourceGenerator.GetTimerNormalized(), 1f, 1f);
        transform.Find("Text").GetComponent<TextMeshPro>().text = Math.Round(resourceGenerator.GetAmountGeneratorPerSecond(),1).ToString("F1");
    }
}
