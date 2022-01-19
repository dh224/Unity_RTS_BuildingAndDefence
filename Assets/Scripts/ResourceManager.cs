using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;
    
    private Dictionary<SO_ResourceType, int> resourceAmountDictionary;
    private SO_ResourceTypeList resourceTypeList;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<SO_ResourceType, int>();
        resourceTypeList =  Resources.Load<SO_ResourceTypeList>(nameof(SO_ResourceTypeList));
        foreach (var resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }
    }
    void Start()
    {
        TestLogRecourseAmountDictionary();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddResource(resourceTypeList.list[0],2);
            TestLogRecourseAmountDictionary();
        }
    }

    private void TestLogRecourseAmountDictionary()
    {
        foreach (var resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(SO_ResourceType resourceType,int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this,EventArgs.Empty);
        TestLogRecourseAmountDictionary();
    }

    public int GetResourceAmount(SO_ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
