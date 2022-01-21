using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName= "ScriptableObjects/BuildingType")]
public class SO_BuildingType :ScriptableObject
{
    public string nameString;
    public Transform prefab;
    [FormerlySerializedAs("recourceGeneratorData")] public ResourceGeneratorData resourceGeneratorData;
    public Sprite buildingSprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
}
